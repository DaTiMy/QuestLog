package com.theodormuenster.questlog

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText

class EditQuest : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_edit_quest)
        if(intent.getBooleanExtra("EXTRA_BOOL",false)==true) {
            val activQuest = intent.getSerializableExtra("EXTRA_QUEST") as QuestData
            findViewById<EditText>(R.id.quest_name).setText(activQuest.name)
            findViewById<EditText>(R.id.copper).setText(activQuest.copper.toString())
            findViewById<EditText>(R.id.silver).setText(activQuest.silver.toString())
            findViewById<EditText>(R.id.gold).setText(activQuest.gold.toString())
            findViewById<EditText>(R.id.exp).setText(activQuest.exp.toString())
        }
        findViewById<Button>(R.id.save_quest).setOnClickListener {
            if(intent.getBooleanExtra("EXTRA_BOOL",false)==true) {
                    for (i in 0..QuestSelection.questList.size-1) {
                        val activQuest = intent.getSerializableExtra("EXTRA_QUEST") as QuestData
                        if(QuestSelection.questList[i].qid == activQuest.qid) {
                            updateQuest(i)
                            QuestSelection.updateRecycler()
                            finish()
                    }
                }
            }
            else {
                var tempqid = 0
                var temporder = 0
                for (i in 0..QuestSelection.questList.size - 1) {
                    if (tempqid <= QuestSelection.questList[i].qid) {
                        tempqid = QuestSelection.questList[i].qid
                    }
                    if (temporder <= QuestSelection.questList[i].ordernumber) {
                        temporder = QuestSelection.questList[i].ordernumber
                    }
                }
                val qid = tempqid
                val order = temporder
                QuestSelection.questList.add(
                    QuestData(
                        findViewById<EditText>(R.id.copper).text.toString().toInt(),
                        findViewById<EditText>(R.id.exp).text.toString().toInt(),
                        0,
                        findViewById<EditText>(R.id.gold).text.toString().toInt(),
                        findViewById<EditText>(R.id.quest_name).text.toString(),
                        order,
                        qid,
                        findViewById<EditText>(R.id.silver).text.toString().toInt()
                    )
                )
                QuestSelection.updateRecycler()
                finish()
                }
            }
        }
    private fun updateQuest(i: Int) {
        QuestSelection.questList[i].copper = findViewById<EditText>(R.id.copper).text.toString().toInt()
        QuestSelection.questList[i].silver = findViewById<EditText>(R.id.silver).text.toString().toInt()
        QuestSelection.questList[i].gold = findViewById<EditText>(R.id.gold).text.toString().toInt()
        QuestSelection.questList[i].exp = findViewById<EditText>(R.id.exp).text.toString().toInt()
        QuestSelection.questList[i].name = findViewById<EditText>(R.id.quest_name).text.toString()
    }
}