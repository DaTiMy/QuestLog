from flask import Blueprint

test_blueprint_1 = Blueprint('test_blueprint', __name__)

@test_blueprint_1.route('/')
def index():
    return "This is an example app"
