package com.frag.q.frag.Fragment;

import android.Manifest;
import android.annotation.TargetApi;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.ContentResolver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.database.Cursor;
import android.os.Build;
import android.os.Bundle;
import android.provider.ContactsContract;
import android.support.annotation.Nullable;

import android.support.v4.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import android.widget.Button;

import com.frag.q.frag.NickNameActivity;
import com.frag.q.frag.R;

public class FragmentA extends Fragment {
    Button btn_name;
    Button btn_nickname1;
    Button btn_nickname2;

    public static FragmentA newInstance(){
        FragmentA fragmentA = new FragmentA();
        return fragmentA;
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState){
        View v = inflater.inflate(R.layout.fragment_a, container, false);

        btn_nickname1 = (Button) v.findViewById(R.id.btn_nickname1);
        btn_nickname1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getActivity(), NickNameActivity.class);
                startActivityForResult(intent, 100);
            }
        });

        btn_nickname2 = (Button) v.findViewById(R.id.btn_nickname2);
        btn_nickname2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getActivity(), NickNameActivity.class);
                startActivityForResult(intent, 100);
            }
        });
        return v;
    }
}