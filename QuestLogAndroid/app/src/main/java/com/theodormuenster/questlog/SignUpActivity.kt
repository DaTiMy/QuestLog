package com.theodormuenster.questlog

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import com.google.android.material.textfield.TextInputLayout

class SignUpActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_sign_up)
        val passwordLayout = findViewById<TextInputLayout>(R.id.registerPasswordTextInputLayout)
        passwordLayout.endIconMode = TextInputLayout.END_ICON_PASSWORD_TOGGLE

        findViewById<Button>(R.id.registerSubmit).setOnClickListener {
            finish()
        }
    }
}