package com.frag.q.frag.Fragment;

import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import com.frag.q.frag.CaptureActivity;
import com.frag.q.frag.GalleryActivity;
import com.frag.q.frag.ImageActivity;
import com.frag.q.frag.R;

public class FragmentB extends Fragment {

    Button btn_capture, btn_album;
    Button button;

    public static FragmentB newInstance(){
        FragmentB fragmentB = new FragmentB();
        return fragmentB;
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_b, container, false);

        btn_capture = (Button) v.findViewById(R.id.btn_capture);
        btn_capture.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getActivity(), CaptureActivity.class);
                startActivityForResult(intent, 100);
            }
        });

        btn_album = (Button) v.findViewById(R.id.btn_album);
        btn_album.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getActivity(), GalleryActivity.class);
                startActivityForResult(intent, 100);
            }
        });
        button = (Button) v.findViewById(R.id.button);
        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getActivity(), ImageActivity.class);
                startActivityForResult(intent, 100);
            }
        });

        return v;
    }
}