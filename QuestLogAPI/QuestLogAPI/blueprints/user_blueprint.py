from typing import Tuple
from flask import Blueprint, Response, Request, jsonify, request
from connect.connect import connection
import collections
import jsonschema
import json
from jsonschema import validate
import bcrypt

user = Blueprint('user', __name__, url_prefix='/users')


userSchema = {

    "properties": {
        "Email": {"type": "integer"},
        "Name": {"type": "string"},
        "Password": {"type": "string"}
        
    },
  "required": ["Email", "Name", "Password"]
}



@user.route('/userlist', methods=['GET', 'POST'])
def get_userlist():
    con  = connection()
    cur = con.cursor()

    cur.execute("SELECT * FROM User")
   
    results = cur.fetchall()
    insertObject = []

    columnNames = [column[0] for column in cur.description]
    for record in results:
        insertObject.append( dict( zip( columnNames , record ) ) )

    con.close()

    return jsonify(insertObject)

@user.route('/userdata/<username>', methods=['GET'])
def get_userdata(username):
    con  = connection()
    cur = con.cursor()
    sql = """SELECT * FROM User WHERE username = %s"""
    cur.execute(sql,(username,))
    results = cur.fetchall()
    insertObject = []

    columnNames = [column[0] for column in cur.description]
    for record in results:
        insertObject.append( dict( zip( columnNames , record ) ) )

   
   
    cur.close()
    con.close()

    return jsonify(insertObject)




@user.route('/register', methods=['POST'])
def insert_register():

    content = request.json

    isValidate = validateRegisterJson(content)

    if not isValidate:
        return quest.register_error_handler(400, handle_bad_request)

    contentDict = json.loads(request.data)

    hashed = hashPassword(contentDict['Password'])

    con = connection()
    cur = con.cursor()
    sql = """INSERT INTO User (Name, Username, Password, Slots) VALUE (%s, %s, %s, %s)"""
    cur.execute(sql,(contentDict['Name'], contentDict['Username'], hashed, 3))
    con.commit()
    cur.close()
    con.close()


    return "Success"



#functions
def hashPassword(passw):
    salt = bcrypt.gensalt()
    hashed = bcrypt.hashpw(passw, salt)

    return hashed



def validateRegisterJson(jsonData):
    try:
        validate(instance=jsonData, schema=questSchema)
    except jsonschema.exceptions.ValidationError as err:
        return False
    return True
