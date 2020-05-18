package com.frag.q.frag.mRecyclerView;

import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.frag.q.frag.R;

import java.util.List;

public class MyAdapter extends RecyclerView.Adapter<ViewHolder> {

    private List<ListItem> listItems;
    private Context context;

    public MyAdapter(List<ListItem> listItems, Context context) {
        this.listItems = listItems;
        this.context = context;
    }

    @Override
    public ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.list_item, parent, false);

        return new ViewHolder(view);
    }

    @Override
    public void onBindViewHolder(ViewHolder holder, int position) {

        ViewHolder viewHolder = (ViewHolder)holder;

        viewHolder.list_title.setText(listItems.get(position).getList_title());
        viewHolder.list_num.setText(listItems.get(position).getList_num());
    }

    @Override
    public int getItemCount() {
        return listItems.size();
    }
}
