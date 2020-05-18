package com.frag.q.frag.Fragment;

import android.content.Intent;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;


import com.frag.q.frag.R;
import com.frag.q.frag.TarotActivity;


public class FragmentC extends Fragment {

    Button btn_tarot;

    public static FragmentC newInstance(){
        FragmentC fragmentC = new FragmentC();
        return fragmentC;
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_c, container, false);

        btn_tarot = (Button) v.findViewById(R.id.btn_tarot);
        btn_tarot.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getActivity(), TarotActivity.class);
                startActivityForResult(intent, 100);
            }
        });
        return v;
    }

}

