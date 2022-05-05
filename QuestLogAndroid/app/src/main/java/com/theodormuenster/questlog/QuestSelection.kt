package com.theodormuenster.questlog

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.Toolbar

class QuestSelection : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_quest_selection)
        findViewById<androidx.appcompat.widget.Toolbar>(R.id.toolbar).title = intent.getStringExtra("STRING_EXTRA")
    }

}