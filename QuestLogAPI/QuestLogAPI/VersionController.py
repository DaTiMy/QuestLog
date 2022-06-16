from connect.connect import connection
import os
import collections
import hashlib
from Migration import Migration as MigrationObject


def main():
    global migrations
    files = listdir_nohidden("../migrations")
    version_list = []
    con = connection()

    sql = "SELECT COUNT(versionID) FROM Migrations"

    cur = con.cursor()
    cur.execute(sql)
    migration_amount = cur.fetchone()
    migration_amount = migration_amount[0]
    cur.close()

    for file in files:
        version = get_versionnumber(get_filename(file))
        version = version[1:]
        version_list.append(int(version))
    version_list.sort()

    if migration_amount != len(version_list):
        migrations = []

        checklist = find_duplicates(version_list)
        if checklist != []:
            print("Duplicated migration(s) found!")
            for element in checklist:
                print("V" + str(element) + "\n")
                exit(1)
        incomplete = check_incomplete(version_list)
        if incomplete:
            print("Migrations incomplete!")
            print("V" + str(incomplete - 1) + " is missing!")
            exit(1)
        files = listdir_nohidden("../migrations")
        files = sorted(files)
        for file in files:
            checksum = create_checksum(file)
            migration = MigrationObject(get_versionnumber(get_filename(file))[1:], get_filename(file), checksum)
            migrations.append(migration)

        if not compare_checksums(con, migrations):
            for element in migrations:
                migration_implementation(con, element)
                db_insert_migration(con, element)

            print("Checksums doesn't match")

    print("Migration check successful!")

    con.close()
    return True


def listdir_nohidden(path):
    for f in os.listdir(path):
        if not f.startswith('.'):
            yield f


def get_filename(path):
    return os.path.splitext(path)[0]


def get_versionnumber(filename):
    return filename.split('_')[0]


def find_duplicates(inputlist):
    check = [item for item, count in collections.Counter(inputlist).items() if count > 1]
    return check


def check_incomplete(inputlist):
    counter = 1
    for element in inputlist:
        if counter > len(inputlist) - 1:
            break
        if inputlist[counter - 1] == inputlist[counter] - 1:
            counter = counter + 1
        else:
            return inputlist[counter]

    return False


def create_checksum(fname):
    hash_md5 = hashlib.md5()
    with open("../migrations/" + fname, "rb") as f:
        for chunk in iter(lambda: f.read(4096), b""):
            hash_md5.update(chunk)
    return hash_md5.hexdigest()


def compare_checksums(con, migrations):
    sql = "SELECT checksum FROM Migrations"
    cur = con.cursor()
    cur.execute(sql)
    checksums = cur.fetchall()
    cur.close()
    if not checksums:
        return False
    if len(checksums) > len(migrations):
        con.close()
        print("V" + str(len(checksums)) + " is missing!")
        exit(1)
    counter = 0
    for migration in migrations:

        if migration.checksum == checksums[counter][0]:
            counter = counter + 1

            if counter > len(checksums) - 1:
                migrations.reverse()
                i = 0
                while i < counter:
                    migrations.pop()
                    i = i + 1
                return False
        else:
            return False

    return True


def db_insert_migration(con, migration):
    cur = con.cursor()
    sql = "INSERT INTO Migrations(versionID, versionName, checksum) VALUES (%s,%s,%s)"
    cur.execute(sql, (migration.versionnumber, migration.versionname, migration.checksum))
    con.commit()
    cur.close()
    return True


def migration_implementation(con, migration):
    cur = con.cursor()
    # file = open("../migrations/" + migration.versionname + ".sql", "r")
    sql_string = ""
    with open("../migrations/" + migration.versionname + ".sql", 'r') as infile:
        for line in infile:
            sql_string += line
            if line.startswith("/"):
                break
    sql_full = sql_string.replace("\n", "")
    sql_statements = sql_full.split(';')
    sql_statements.pop()
    for statement in sql_statements:
        sql = statement + ";"
        cur.execute(sql)

    con.commit()

    cur.close()


if __name__ == '__main__':
    main()
