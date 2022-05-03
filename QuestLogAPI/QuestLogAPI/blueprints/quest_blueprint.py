from typing import Tuple
from flask import Blueprint, Response, Request, jsonify, request
from connect.connect import connection
import jsonschema
import json
from jsonschema import validate

quest = Blueprint('quest_blueprint', __name__, url_prefix='/quests')


questSchema = {

    "properties": {
        "Copper": {"type": "string"},
        "EXP": {"type": "integer"},
        "Gold": {"type": "integer"},
        "Name": {"type": "string"},
        "SID": {"type": "integer"},
        "Silver": {"type": "integer"},
    },
  "required": ["Copper", "EXP", "Gold", "Name", "SID", "Silver"]
}

subQuestSchema = {

    "properties": {
        "Description": {"type": "string"},
        "Name": {"type": "string"},
        "QID": {"type": "integer"},
    },
  "required": ["Description", "Name", "QID"]
}


#region routes

@quest.route('/add/quest', methods=['POST'])
def addQuest():
    content = request.json

    isValidate = validateQuestJson(content)

    if not isValidate:
        return quest.register_error_handler(400, handle_bad_request)
   
    contentDict = json.loads(request.data)

    sql = """INSERT INTO Quest (sid,name,exp,copper,silver,gold,finish,ordernumber) VALUES (%s, %s, %s, %s, %s, %s, %s, %s)"""

    con  = connection()
    cur = con.cursor()
    contentDict = contentDict[0]
    cur.execute(sql,(contentDict['SID'], contentDict['Name'], contentDict['EXP'], contentDict['Copper'], contentDict['Silver'], contentDict['Gold'], 0, setNewOrderNumber(con,contentDict['SID'])))
    con.commit()
    con.close()

        
    res = jsonify(success=True)
    return res



@quest.route('/add/subquest', methods=['POST'])
def addSubQuest():
    content = request.json

    isValidate = validateSubQuestJson(content)

    if not isValidate:
        return quest.register_error_handler(400, handle_bad_request)
   
    contentDict = json.loads(request.data)

    sql = """INSERT INTO SubGoal (qid,name,description,finish) VALUES (%s, %s, %s, %s)"""

    con  = connection()
    cur = con.cursor()
    contentDict = contentDict[0]
    cur.execute(sql,(contentDict['QID'], contentDict['Name'], contentDict['Description'],0))
    con.commit()
    con.close()

        
    res = jsonify(success=True)
    return res


@quest.route('/delete/quest/<QID>', methods=['DELETE'])
def deleteQuest(QID):


    sql = """DELETE FROM Quest WHERE qid = %s"""

    con  = connection()
    cur = con.cursor()
    cur.execute(sql,(QID,))
    con.commit()
    con.close()

        
    res = jsonify(success=True)
    return res



@quest.route('/delete/subquest/<SGID>', methods=['DELETE'])
def deleteSubQuest(SGID):


    sql = """DELETE FROM SubGoal WHERE sgid = %s"""

    con  = connection()
    cur = con.cursor()
    cur.execute(sql,(SGID,))
    con.commit()
    con.close()

        
    res = jsonify(success=True)
    return res





@quest.route('/sort/<SID>', methods=['GET'])
def sort(SID):
    content = request.data
    contentArr = content.decode().split(',')
    sql = """SELECT qid FROM Quest WHERE sid = %s ORDER BY qid"""
    con = connection()
    cur = con.cursor()
    cur.execute(sql,(SID,))
    result = [item[0] for item in cur.fetchall()]
    
    insertDict = {}
    counter = 0

    for element in result:
        insertDict[element] = contentArr[counter]  
        counter+=1
    sql = """UPDATE Quest SET ordernumber = %s WHERE qid = %s"""
    
    for key in insertDict.keys():
        cur.execute(sql,(insertDict[key],key))
        con.commit()
      
    con.close()


    res = jsonify(success=True)
    res.status_code = 200
    return res




@quest.route('/select/qid/<QID>', methods=['GET'])
def select(QID):
    sql = """SELECT qid, name, exp, copper, silver, gold, finish, ordernumber FROM Quest WHERE qid = %s"""
    con  = connection()
    cur = con.cursor()
    cur.execute(sql,(QID,))
    results = cur.fetchall()
    insertObject = []

    columnNames = [column[0] for column in cur.description]
    for record in results:
        insertObject.append( dict( zip( columnNames , record ) ) )
 
    con.close()
 
    return jsonify(insertObject)



@quest.route('/select/sid/<SID>', methods=['GET'])
def selectQSID(SID):
    
    sql = """SELECT qid, name, exp, copper, silver, gold, finish, ordernumber FROM Quest WHERE sid = %s"""
    con  = connection()
    cur = con.cursor()
    cur.execute(sql,(SID,))
    results = cur.fetchall()
    insertObject = []
  
    columnNames = [column[0] for column in cur.description]
    for record in results:
        temp = dict( zip( columnNames , record ))
        temp['subquests'] = fetchSubQuests(con,record[0])
        
        insertObject.append(temp )
      
       
    con.close()
    
    return jsonify(insertObject)




@quest.route('/select/all', methods=['GET'])
def selectAll():
    sql = """SELECT qid, name, exp, copper, silver, gold, finish, ordernumber FROM Quest"""
    con  = connection()
    cur = con.cursor()
    cur.execute(sql)
    results = cur.fetchall()
    insertObject = []

    columnNames = [column[0] for column in cur.description]
    for record in results:
       
        insertObject.append( dict( zip( columnNames , record ) ) )
 
    con.close()
 
    return jsonify(insertObject)


@quest.route('/finish/quest/<QID>', methods=['GET'])
def finishQuest():
    sql = 




#endregion




def validateQuestJson(jsonData):
    try:
        validate(instance=jsonData, schema=questSchema)
    except jsonschema.exceptions.ValidationError as err:
        return False
    return True

def validateSubQuestJson(jsonData):
    try:
        validate(instance=jsonData, schema=subQuestSchema)
    except jsonschema.exceptions.ValidationError as err:
        return False
    return True


def setNewOrderNumber(con,SID):
    sql = """SELECT ordernumber FROM Quest WHERE sid = %s"""
    
    cur = con.cursor()
    cur.execute(sql,(SID,))
    results = cur.fetchall()



    return len(results)+1
    


def setOrderNumber(Arr):
    return sorted(Arr)



def fetchSubQuests(con,QID):
    sql = """SELECT sgid, name, description, finish FROM SubGoal WHERE qid = %s"""

    cur = con.cursor()
    cur.execute(sql,(QID,))
    results = cur.fetchall()
   
    cur.close()
    
    insertObject = []
    if len(results) < 1:
        return None
    columnNames = [column[0] for column in cur.description]
    for record in results:
        insertObject.append( dict( zip( columnNames , record ) ) )
 
    return insertObject


