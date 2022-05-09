package com.theodormuenster.questlog

import android.content.Intent
import android.media.Image
import android.transition.AutoTransition
import android.transition.TransitionManager
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.CheckBox
import android.widget.ImageView
import android.widget.TextView
import androidx.cardview.widget.CardView
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView

class QuestSelectionAdapter(private val questList: ArrayList<QuestData>) : RecyclerView.Adapter<QuestSelectionAdapter.QuestSelectionViewHolder>(){
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): QuestSelectionViewHolder {
        val itemView = LayoutInflater.from(parent.context).inflate(R.layout.quest_item,parent,false)
        return QuestSelectionViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: QuestSelectionViewHolder, position: Int) {
        val currentItem = questList[position]
        holder.questName.text = currentItem.name
        holder.gold.text = "GP: ${currentItem.gold}"
        holder.silver.text = "SP: ${currentItem.silver}"
        holder.copper.text = "CP: ${currentItem.copper}"
        holder.exp.text = "EXP: ${currentItem.exp}"
        holder.bind()
        holder.expandView.setOnClickListener {
            val expandableView = holder.itemView.findViewById<ConstraintLayout>(R.id.expandableView)
            if(expandableView.visibility== View.GONE) {
                TransitionManager.beginDelayedTransition(holder.itemView.findViewById<CardView>(R.id.expandableCardview),AutoTransition())
                expandableView.visibility = View.VISIBLE
            }
            else {
                TransitionManager.beginDelayedTransition(holder.itemView.findViewById<CardView>(R.id.expandableCardview),AutoTransition())
                expandableView.visibility = View.GONE
            }
        }
        holder.editQuest.setOnClickListener {
            val intent = Intent(holder.itemView.context, EditQuest::class.java).apply {
                putExtra("EXTRA_QUEST",questList[position])
                putExtra("EXTRA_BOOL",true)
            }
            holder.itemView.context.startActivity(intent)
        }
    }

    override fun getItemCount(): Int {
        return questList.size
    }

    class QuestSelectionViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        val questCheck = itemView.findViewById<CheckBox>(R.id.quest_check)
        val questName = itemView.findViewById<TextView>(R.id.quest_name)
        val expandView = itemView.findViewById<ImageView>(R.id.expand_arrow)
        val subquestRecycler = itemView.findViewById<RecyclerView>(R.id.nested_quest_recycler)
        val copper = itemView.findViewById<TextView>(R.id.copper)
        val silver = itemView.findViewById<TextView>(R.id.silver)
        val gold = itemView.findViewById<TextView>(R.id.gold)
        val exp = itemView.findViewById<TextView>(R.id.exp)
        val addSubquest = itemView.findViewById<ImageView>(R.id.add_subquest)
        val editQuest = itemView.findViewById<ImageView>(R.id.edit_quest)

        val subquestList = ArrayList<SubquestData>()

        private fun fillSubquest() {
            subquestList.clear()
            subquestList.add(SubquestData("Subgoal1 ", "This is the first Subgoal",1))
            subquestList.add(SubquestData("Subgoal2 ", "This is the second Subgoal",1))
            subquestList.add(SubquestData("Subgoal2 ", "This is the thirst Subgoal",1))
        }

        fun bind() {
            val childRecycler = itemView.findViewById<RecyclerView>(R.id.nested_quest_recycler)
            childRecycler.layoutManager = LinearLayoutManager(itemView.context,
                LinearLayoutManager.VERTICAL,false)
            fillSubquest()
            childRecycler.adapter = SubquestAdapter(subquestList)
        }
    }

}