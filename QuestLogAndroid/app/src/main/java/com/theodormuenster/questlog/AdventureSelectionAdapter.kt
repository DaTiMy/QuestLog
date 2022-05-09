package com.theodormuenster.questlog

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView

class AdventureSelectionAdapter(private val AdventureList: ArrayList<AdventureData>,private val onItemClick: (position: Int)-> Unit): RecyclerView.Adapter<AdventureSelectionAdapter.AdventureSelectionViewHolder>() {

    class AdventureSelectionViewHolder(itemView: View,
                                       private val onItemClick: (position: Int) -> Unit
                                       ) : RecyclerView.ViewHolder(itemView), View.OnClickListener {

        val campaignName = itemView.findViewById<TextView>(R.id.campaignName)
        val dmName = itemView.findViewById<TextView>(R.id.dmName)
        init {
            itemView.setOnClickListener({_ -> onItemClick(adapterPosition)})
        }

        override fun onClick(v: View?) {

        }

    }

    override fun onCreateViewHolder(
        parent: ViewGroup,
        viewType: Int,
    ): AdventureSelectionViewHolder {
        val itemView = LayoutInflater.from(parent.context).inflate(R.layout.campaign_element,parent,false)
        return AdventureSelectionViewHolder(itemView,onItemClick)
    }

    override fun onBindViewHolder(holder: AdventureSelectionViewHolder, position: Int) {
        val currentItem = AdventureList[position]
        holder.campaignName.text = currentItem.campaignName
        holder.dmName.text = "DM:${currentItem.campaignDM}"

    }

    override fun getItemCount(): Int {
        return AdventureList.size
    }
}