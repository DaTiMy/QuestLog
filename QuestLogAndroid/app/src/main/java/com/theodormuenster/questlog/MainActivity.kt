package com.theodormuenster.questlog

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.MotionEvent
import android.view.View
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import com.google.android.material.textfield.TextInputLayout

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        val passwordLayout = findViewById<TextInputLayout>(R.id.passwordTextInputLayout)
        passwordLayout.endIconMode = TextInputLayout.END_ICON_PASSWORD_TOGGLE
        findViewById<Button>(R.id.login).setOnClickListener {
            val intent = Intent(this, AdventureSelection::class.java)
            startActivity(intent)
        }
        findViewById<Button>(R.id.register).setOnClickListener {
            val intent = Intent(this,SignUpActivity::class.java)
            startActivity(intent)
        }
    }
}
