package com.frag.q.frag.mRecyclerView;

public class ListItem {

    private String list_title;
    private String list_num;

    public ListItem(String list_title, String list_num) {
        this.list_title = list_title;
        this.list_num = list_num;
    }

    public String getList_title() {
        return list_title;
    }

    public String getList_num() {
        return list_num;
    }
}
