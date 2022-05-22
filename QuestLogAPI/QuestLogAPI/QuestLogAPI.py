from flask import Flask
from connect.connect import connection

#Blueprints
from blueprints.test_blueprint import test_blueprint_1
from blueprints.user_blueprint import user
from blueprints.quest_blueprint import quest

app = Flask(__name__)

app.register_blueprint(user)
app.register_blueprint(quest)
app.register_blueprint(test_blueprint_1)






def link(sql):

    cur = con.cursor()
    cur.execute(sql)
    data = cur.fetchall()
    return jsonify(data)


if __name__ == "__main__":
    app.run(host='0.0.0.0',port=5000)