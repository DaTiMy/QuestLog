package com.theodormuenster.questlog

import java.io.Serializable

class QuestData(var copper: Int, var exp: Int,var finish: Int,var gold :Int,
                     var name:String,var ordernumber: Int,val qid:Int, var silver: Int) : Serializable
