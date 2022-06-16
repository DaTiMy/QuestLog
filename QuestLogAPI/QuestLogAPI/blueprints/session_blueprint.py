from typing import Tuple
from flask import Blueprint, Response, Request, jsonify, request
from connect.connect import connection
import jsonschema
import json
from jsonschema import validate

session = Blueprint('session', __name__, url_prefix='/sessions')

sessionSchema = {

    "properties": {
        "Name": {"type": "integer"},
        "Password": {"type": "string"},
        "UID": {"type": "int"},
        "isDM": {"type": "int"}

    },
    "required": ["Email", "Name", "Password", "Username"]
}


@session.route('createsession', methods=['POST'])
def createSession():
    content = request.json

    res = jsonify(success=True)
    return res
