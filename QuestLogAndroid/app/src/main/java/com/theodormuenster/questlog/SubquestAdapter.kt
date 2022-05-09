package com.theodormuenster.questlog

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView

class SubquestAdapter(private val subquestList: ArrayList<SubquestData>): RecyclerView.Adapter<SubquestAdapter.SubquestViewHolder>() {
    class SubquestViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        val subquestName = itemView.findViewById<TextView>(R.id.subquest_name)
        val subquestDescription = itemView.findViewById<TextView>(R.id.description)

    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): SubquestViewHolder {
        val itemView = LayoutInflater.from(parent.context).inflate(R.layout.subquest_item,parent,false)
        return SubquestViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: SubquestViewHolder, position: Int) {
        val currentItem = subquestList[position]
        holder.subquestName.text = currentItem.name
        holder.subquestDescription.text = currentItem.description


    }

    override fun getItemCount(): Int {
        return subquestList.size
    }
}