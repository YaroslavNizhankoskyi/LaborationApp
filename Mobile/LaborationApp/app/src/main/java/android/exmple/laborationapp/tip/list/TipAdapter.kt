package android.exmple.laborationapp.tip.list

import android.exmple.laborationapp.R
import android.exmple.laborationapp.databinding.TipItemBinding
import android.exmple.laborationapp.models.UserTip
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.databinding.DataBindingUtil
import androidx.recyclerview.widget.RecyclerView

class TipAdapter(
    private val tips: List<UserTip>
) : RecyclerView.Adapter<TipAdapter.TipViewHolder>(){

    override fun getItemCount() = tips.size

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int) =
        TipViewHolder(
            DataBindingUtil.inflate(
                LayoutInflater.from(parent.context),
                R.layout.tip_item,
                parent,
                false
            )
        )

    override fun onBindViewHolder(holder: TipViewHolder, position: Int) {
        holder.recyclerviewMovieBinding.tip = tips[position]
    }


    inner class TipViewHolder(
        val recyclerviewMovieBinding: TipItemBinding
    ) : RecyclerView.ViewHolder(recyclerviewMovieBinding.root)

}