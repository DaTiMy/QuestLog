import datetime
from typing import Tuple
from flask import Blueprint, Response, Request, jsonify, request
from flask_httpauth import HTTPBasicAuth
from connect.connect import connection
import collections
import jsonschema
import json
from jsonschema import validate
import bcrypt

user = Blueprint('user', __name__, url_prefix='/users')
auth = HTTPBasicAuth()

userSchema = {

    "properties": {
        "Email": {"type": "integer"},
        "Name": {"type": "string"},
        "Password": {"type": "string"},
        "Username": {"type": "string"}

    },
    "required": ["Email", "Name", "Password", "Username"]
}
loginSchema = {

    "properties": {
        "Email": {"type": "integer"},
        "Password": {"type": "string"},
        "Username": {"type": "string"}

    },
    "required": ["Password"]
}


@user.route('/userlist', methods=['GET', 'POST'])
def get_userlist():
    con = connection()
    cur = con.cursor()

    cur.execute("SELECT * FROM User")

    results = cur.fetchall()
    insertObject = []

    columnNames = [column[0] for column in cur.description]
    for record in results:
        insertObject.append(dict(zip(columnNames, record)))

    con.close()

    return jsonify(insertObject)


@user.route('/userdata/<username>', methods=['GET'])
def get_userdata(username):
    con = connection()
    cur = con.cursor()
    sql = """SELECT * FROM User WHERE username = %s"""
    cur.execute(sql, (username,))
    results = cur.fetchall()
    insertObject = []

    columnNames = [column[0] for column in cur.description]
    for record in results:
        insertObject.append(dict(zip(columnNames, record)))

    cur.close()
    con.close()

    return jsonify(insertObject)


@user.route('/register', methods=['POST'])
def insert_register():
    content = request.json

    isValidate = validateRegisterJson(content)

    if not isValidate:
        return user.register_error_handler(400, handle_bad_request)

    contentDict = json.loads(request.data)
    contentDict = contentDict[0]

    con = connection()
    cur = con.cursor()

    sql = """SELECT Username FROM User WHERE Username = %s OR Email = %s"""
    cur.execute(sql, (contentDict['Username'], contentDict['Email']))
    result = cur.fetchall()

    if len(result) > 0:
        return "User or Email already exists!"

    sql = """INSERT INTO User (Name, Email, Username, Password, Slots) VALUE (%s, %s, %s, %s, %s)"""
    hashed = hashPassword(contentDict['Password'])
    cur.execute(sql, (contentDict['Name'], contentDict['Email'], contentDict['Username'], hashed, 3))
    con.commit()
    cur.close()
    con.close()

    res = jsonify(success=True)
    return res


@user.route('/login', methods=['POST'])
def login():
    content = request.json
    user = None
    isValidate = validateLoginJson(content)
    if not isValidate:
        return user.register_error_handler(400, handle_bad_request)

    contentDict = json.loads(request.data)
    contentDict = contentDict[0]
    con = connection()
    if 'Username' in contentDict:
        user = contentDict['Username']
    else:
        user = None

    if 'Email' in contentDict:
        email = contentDict['Email']
    else:
        email = None

    exists = verify_user(con, user, email)
    if not exists:
        con.close()
        return "User or Email doesn't exists!"
    sql = """SELECT Password FROM User WHERE Username = %s OR Email = %s"""
    cur = con.cursor()
    cur.execute(sql, (user, email))
    result = cur.fetchall()
    # tuple
    result = result[0][0]
    if not bcrypt.checkpw(contentDict['Password'].encode('utf-8'), result.encode('utf-8')):
        cur.close()
        con.close()
        return "Wrong Password!"
    sql = """UPDATE User SET SessionToken = %s, LastLoginDate = %s WHERE Username = %s OR Email = %s"""
    token = ""
    if user is not None:
        token = user + "ToKeN"
    if email is not None:
        token = email + "ToKeN"
    date = datetime.date.ctime()
    cur.execute(sql, (token, date, user, email))
    con.commit()
    cur.close()
    con.close()

    res = jsonify(success=True)
    return res


# functions
def verify_user(con, user=None, email=None):
    cur = con.cursor()

    sql = """SELECT Username FROM User WHERE Username = %s OR Email = %s"""
    cur.execute(sql, (user, email))
    result = cur.fetchall()
    cur.close()
    if len(result) < 1:
        return False

    return True


def hashPassword(passw):
    salt = bcrypt.gensalt()
    hashed = bcrypt.hashpw(passw.encode('utf8'), salt)

    return hashed


def validateRegisterJson(jsonData):
    try:
        validate(instance=jsonData, schema=userSchema)
    except jsonschema.exceptions.ValidationError as err:
        return False
    return True


def validateLoginJson(jsonData):
    try:
        validate(instance=jsonData, schema=loginSchema)
    except jsonschema.exceptions.ValidationError as err:
        return False
    return True

def handle_bad_request():
    res = jsonify(success=False)
    res.status_code = 400

    return res