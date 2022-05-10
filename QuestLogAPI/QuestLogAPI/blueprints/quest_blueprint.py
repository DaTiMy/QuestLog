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
    cur.close()
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

    sql = """INSERT INTO SubGoal (qid,name,description,finish,ordernumber) VALUES (%s, %s, %s, %s, %s)"""

    con  = connection()
    cur = con.cursor()
    contentDict = contentDict[0]
    cur.execute(sql,(contentDict['QID'], contentDict['Name'], contentDict['Description'],0, setNewSubOrderNumber(con,contentDict['QID'])))
    con.commit()
    cur.close()
    con.close()

        
    res = jsonify(success=True)
    return res


@quest.route('/delete/quest/<QID>', methods=['DELETE'])
def deleteQuest(QID):
    con  = connection()
    cur = con.cursor()
    sql = """SELECT sid,ordernumber FROM Quest WHERE qid = %s"""

    cur.execute(sql,(QID,))
    result = cur.fetchall()

    result = result[0]

   

    sql = """DELETE FROM Quest WHERE qid = %s"""


    cur.execute(sql,(QID,))
    con.commit()

    rearrangeQuestOrder(con,result[0],result[1])

    cur.close()
    con.close()

        
    res = jsonify(success=True)
    return res



@quest.route('/delete/subquest/<SGID>', methods=['DELETE'])
def deleteSubQuest(SGID):
    con  = connection()
    cur = con.cursor()
    sql = """SELECT qid,ordernumber FROM SubGoal WHERE sqid = %s"""

    cur.execute(sql,(QID,))
    result = cur.fetchall()

    result = result[0]


    sql = """DELETE FROM SubGoal WHERE sgid = %s"""

    cur.execute(sql,(SGID,))
    con.commit()

    rearrangeSubQuestOrder(con,result[0],result[1])

    cur.close()
    con.close()

        
    res = jsonify(success=True)
    return res





@quest.route('/sort/quest/<SID>', methods=['GET'])
def sortQuests(SID):
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
    cur.close()      
    con.close()


    res = jsonify(success=True)
    res.status_code = 200
    return res

@quest.route('/sort/subquest/<QID>', methods=['GET'])
def sortSubQuests(QID):
    content = request.data
    contentArr = content.decode().split(',')
    sql = """SELECT sgid FROM SubGoal WHERE qid = %s ORDER BY sgid"""
    con = connection()
    cur = con.cursor()
    cur.execute(sql,(QID,))
    result = [item[0] for item in cur.fetchall()]
    
    insertDict = {}
    counter = 0

    for element in result:
        insertDict[element] = contentArr[counter]  
        counter+=1
    sql = """UPDATE SubGoal SET ordernumber = %s WHERE sgid = %s"""
    
    for key in insertDict.keys():
        cur.execute(sql,(insertDict[key],key))
        con.commit()
    cur.close()      
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
    
    cur.close()
    con.close()
 
    return jsonify(insertObject)



@quest.route('/select/sid/<SID>', methods=['GET'])
def selectQSID(SID):
    
    sql = """SELECT qid, name, exp, copper, silver, gold, finish, ordernumber FROM Quest WHERE sid = %s ORDER BY ordernumber"""
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
      
    cur.close()      
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
 
    cur.close()
    con.close()
 
    return jsonify(insertObject)


@quest.route('/finish/quest/<QID>', methods=['PATCH'])
def finishQuest(QID):
    sql = """SELECT finish FROM Quest WHERE qid = %s"""
    con  = connection()
    cur = con.cursor()
    cur.execute(sql,(QID,))
    results = cur.fetchall()
    
    if results[0][0] == 0:
        sql = """UPDATE Quest SET finish = 1 WHERE qid = %s"""
    else:
        sql = """UPDATE Quest SET finish = 0 WHERE qid = %s"""
    cur.execute(sql,(QID,))
    con.commit()
    cur.close()
    con.close()

    res = jsonify(success=True)
    res.status_code = 200

    return res

@quest.route('/finish/subquest/<SGID>', methods=['PATCH'])
def finishSubQuest(SGID):
    sql = """SELECT finish FROM SubGoal WHERE sgid = %s"""
    con  = connection()
    cur = con.cursor()
    cur.execute(sql,(SGID,))
    results = cur.fetchall()
    
    if results[0][0] == 0:
        sql = """UPDATE SubGoal SET finish = 1 WHERE sgid = %s"""
    else:
        sql = """UPDATE SubGoal SET finish = 0 WHERE sgid = %s"""
    cur.execute(sql,(SGID,))
    con.commit()
    cur.close()
    con.close()

    res = jsonify(success=True)
    res.status_code = 200

    return res


@quest.route('/update/quest/<QID>', methods=['PATCH'])
def updateQuest(QID):

    counter = 0
    sql = """UPDATE Quest SET """
    contentDict = json.loads(request.data)
    contentDict = contentDict[0]
    #Null checks
    if "Name" in contentDict:
        sql+= "Name=" + "'"+contentDict['Name']+"'"
        counter+=1
    if "EXP" in contentDict:
        if counter > 0:
            sql+= ", EXP=" + str(contentDict['EXP'])
        else:
            sql+= "EXP=" + str(contentDict['EXP'])
        counter+=1
    if "Copper" in contentDict:
        if counter > 0:
            sql+= ", Copper=" + str(contentDict['Copper'])
        else:
            sql+= "Copper=" + str(contentDict['Copper'])
        counter+=1
    if "Silver" in contentDict:
        if counter > 0:
            sql+= ", Silver=" + str(contentDict['Silver'])
        else:
            sql+= "Silver=" + str(contentDict['Silver'])
        counter+=1
    if "Gold" in contentDict:
        if counter > 0:
            sql+= ", Gold=" + str(contentDict['Gold'])
        else:
            sql+= "Gold=" + str(contentDict['Gold'])
        counter+=1
    if counter < 1:
        res = jsonify(success=False)
        res.status_code = 400

        return res

    sql += """ WHERE qid=%s"""

    
    con  = connection()
    cur = con.cursor()
    cur.execute(sql,(QID,))
    con.commit()
    cur.close()
    con.close()

    res = jsonify(success=True)
    res.status_code = 200

    return res


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

    cur.close()

    return len(results)+1
    
def setNewSubOrderNumber(con,QID):
    sql = """SELECT ordernumber FROM SubGoal WHERE QID = %s"""
    
    cur = con.cursor()
    cur.execute(sql,(QID,))
    results = cur.fetchall()

    cur.close()

    return len(results)+1


def setOrderNumber(Arr):
    return sorted(Arr)


def rearrangeQuestOrder(con,SID,delOrderNumber):

    cur = con.cursor()
    #1. SID, 2. delOrderNumber
    sql = """UPDATE Quest SET ordernumber = (ordernumber - 1) WHERE sid = %s AND ordernumber > %s"""
    cur.execute(sql,(SID,delOrderNumber))
    con.commit()
    cur.close()

    return True





def rearrangeSubQuestOrder(con,QID,delOrderNumber):

    cur = con.cursor()
    #1. SID, 2. delOrderNumber
    sql = """UPDATE SubGoal SET ordernumber = (ordernumber - 1) WHERE qid = %s AND ordernumber > %s"""
    cur.execute(sql,(QID,delOrderNumber))
    con.commit()
    cur.close()

    return True


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


def get_missing_summatin(a):
  n = a[-1]
  total = n*(n+1)//2
  summation = 0
  summation = sum(a)
  result = total-summation
  print(result)
