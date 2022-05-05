package com.theodormuenster.questlog

import android.content.DialogInterface
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.text.InputType
import android.widget.EditText
import android.widget.ImageView
import androidx.appcompat.app.AlertDialog
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView

class AdventureSelection : AppCompatActivity() {
    private lateinit var adventureRecycler: RecyclerView
    private lateinit var adventureList : ArrayList<AdventureData>

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_adventure_selection)
        adventureRecycler = findViewById(R.id.adventureRecycler)
        adventureRecycler.layoutManager = LinearLayoutManager(this)
        adventureRecycler.setHasFixedSize(false)
        adventureList = fillAdventureList()
        adventureRecycler.adapter = AdventureSelectionAdapter(adventureList)
        findViewById<ImageView>(R.id.plusButton).setOnClickListener {
            showNameInput()
        }
    }

    private fun showNameInput() {
        val builder = AlertDialog.Builder(this,R.style.)

        builder.setTitle("New Campaign")

        val input = EditText(this)
        input.setHint("Enter Campaign Name")
        input.inputType = InputType.TYPE_CLASS_TEXT
        builder.setView(input)
        builder.setPositiveButton("Add",DialogInterface.OnClickListener {
            dialog, which ->
            var tempCampaignName = input.text.toString()

            // TODO Get Username from Login
            adventureList.add(AdventureData(tempCampaignName,"Theo"))

        })
        builder.setNegativeButton("Cancel",DialogInterface.OnClickListener{dialog, which ->  dialog.cancel()})
        builder.show()
    }

    private fun fillAdventureList(): ArrayList<AdventureData> {
        val advData = AdventureData("Die Kolonie des Eisens","Theo")
        val tempList : ArrayList<AdventureData> = ArrayList<AdventureData>()
        tempList.add(advData)
        return tempList
    }
}