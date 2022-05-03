from typing import Tuple
from flask import Blueprint, Response, Request, jsonify
from connect.connect import connection
import collections

user = Blueprint('user', __name__, url_prefix='/users')



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

   
   
 
    con.close()

    return jsonify(insertObject)




@user.route('/register', methods=['POST'])
def insert_register():

    return "Success"
