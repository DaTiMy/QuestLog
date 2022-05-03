import mysql.connector
from mysql.connector import errorcode

def connection():
    try:
      cnx = mysql.connector.connect(user='db_quser', 
                                    password='d8BYAntt',
                                    host='dbquestlog.ccow0wltnjlo.eu-central-1.rds.amazonaws.com',
                                    database='db_questlog')
    except mysql.connector.Error as err:
      if err.errno == errorcode.ER_ACCESS_DENIED_ERROR:
        print("Something is wrong with your user name or password")
      elif err.errno == errorcode.ER_BAD_DB_ERROR:
        print("Database does not exist")
      else:
        print(err)
    else:
      print("connected!")

      return cnx