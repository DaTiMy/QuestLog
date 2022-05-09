package com.theodormuenster.questlog

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.ImageView
import android.widget.Toolbar
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView

class QuestSelection : AppCompatActivity() {
    //private lateinit var questRecycler: RecyclerView

    companion object {
        var questList = fillQuestList()
            fun fillQuestList() : ArrayList<QuestData>{
            val quest =QuestData(0,0,0,0,"TestQuest",1,1,0)
            val templist = ArrayList<QuestData>()
            templist.add(quest)
            return templist
        }
        lateinit var questRecycler: RecyclerView
        fun updateRecycler() {
            questRecycler.adapter!!.notifyDataSetChanged()
        }
    }
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_quest_selection)
        findViewById<androidx.appcompat.widget.Toolbar>(R.id.toolbar).title = intent.getStringExtra("EXTRA_STRING")
        questRecycler = findViewById(R.id.quest_recycler)
        questRecycler.layoutManager = LinearLayoutManager(this)
        questRecycler.setHasFixedSize(false)
        questList= fillQuestList()
        questRecycler.adapter = QuestSelectionAdapter(questList)
        findViewById<ImageView>(R.id.add_quest).setOnClickListener {
            val intent = Intent(this,EditQuest::class.java).putExtra("EXTRA_BOOLEAN",false)
            startActivity(intent)
        }

    }



}