from flask import Blueprint, redirect

test_blueprint_1 = Blueprint('test_blueprint', __name__)

@test_blueprint_1.route('/')
def index():
    return redirect("https://app.ilert.com/api/developers/oauth2/authorize?client_id=2b1e0c763904826c0b3a&response_type=code&scope=profile%20source%3Ar&email")
