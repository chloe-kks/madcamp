package com.frag.q.frag.mRecyclerView;

import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.TextView;

import com.frag.q.frag.R;


public class ViewHolder extends RecyclerView.ViewHolder {

    public TextView list_title;
    public TextView list_num;

    public ViewHolder(View itemView) {

        super(itemView);

        list_title = (TextView) itemView.findViewById(R.id.list_title);
        list_num = (TextView) itemView.findViewById(R.id.list_num);
    }
}
