package com.frag.q.frag;

import android.Manifest;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.speech.RecognitionListener;
import android.speech.RecognizerIntent;
import android.speech.SpeechRecognizer;
import android.speech.tts.TextToSpeech;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.Locale;

public class TarotActivity extends AppCompatActivity {
    Intent intent;
    SpeechRecognizer mRecognizer;
    TextView textView;
    TextToSpeech tts;
    private final int MY_PERMISSIONS_RECORD_AUDIO = 1;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_tarot);
        if (ContextCompat.checkSelfPermission(this,
                Manifest.permission.RECORD_AUDIO)
                != PackageManager.PERMISSION_GRANTED) {

            if (ActivityCompat.shouldShowRequestPermissionRationale(this,
                    Manifest.permission.RECORD_AUDIO)) {

            } else {
                ActivityCompat.requestPermissions(this,
                        new String[]{Manifest.permission.RECORD_AUDIO}, MY_PERMISSIONS_RECORD_AUDIO
                );
            }
        }

        tts = new TextToSpeech(this, new TextToSpeech.OnInitListener() {
            @Override
            public void onInit(int status) {
                tts.setLanguage(Locale.KOREAN);
            }
        });

        intent = new Intent(RecognizerIntent.ACTION_RECOGNIZE_SPEECH);
        intent.putExtra(RecognizerIntent.EXTRA_CALLING_PACKAGE, getPackageName());
        intent.putExtra(RecognizerIntent.EXTRA_LANGUAGE, "ko-KR");

        mRecognizer = SpeechRecognizer.createSpeechRecognizer(this);
        mRecognizer.setRecognitionListener(recognitionListener);


        textView = (TextView) findViewById(R.id.textView);

        Button button = (Button) findViewById(R.id.button01);
        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                mRecognizer.startListening(intent);
            }
        });

    }

    private void replyAnswer(String input, TextView txt){
        try{
            String cmd = input.split(" ")[0];

            if(cmd.equals("검색")){
                String data = input.replace("검색 ","");
                txt.append("[Siri] : "+ data +"에 대한 검색 결과입니다.\n");
                tts.speak(data + "에 대한 검색 결과입니다.", TextToSpeech.QUEUE_FLUSH, null);
                Intent intent = new Intent(this, WebActivity.class);
                intent.putExtra("value", data);
                startActivity(intent);
            }
            else if(input.equals("시리야")){
                txt.append("[Siri] : 네, 부르셨어요?\n");
                tts.speak("네 부르셨어요", TextToSpeech.QUEUE_FLUSH, null);
            }else if(input.equals("너는 누구니")){
                txt.append("[Siri] : Siri입니다. 누구세요?\n");
                tts.speak("시리입니다. 누구세요?", TextToSpeech.QUEUE_FLUSH, null);
            }else if(input.equals("시리야 오늘 날씨 어때")) {
                txt.append("[Siri] : 오늘의 대전 날씨를 보여드릴게요.\n");
                tts.speak("오늘의 대전 날씨를 보여드릴게요.", TextToSpeech.QUEUE_FLUSH, null);
                Intent intent = new Intent(this, WebActivity.class);
                intent.putExtra("value", "대전 날씨");
                startActivity(intent);
            }else if(input.equals("날씨")) {
                txt.append("[Siri] : 오늘의 전국 날씨입니다.\n");
                tts.speak("오늘의 전국 날씨입니다.", TextToSpeech.QUEUE_FLUSH, null);
                Intent intent = new Intent(this, WebActivity.class);
                intent.putExtra("value", "전국 날씨");
                startActivity(intent);
            }else if(input.equals("매드캠프 어때")) {
                txt.append("[Siri] : 재미있어요.\n");
                tts.speak("재미있어요", TextToSpeech.QUEUE_FLUSH, null);
            }else if(input.equals("좋은 하루 보내")) {
                txt.append("[Siri] : 네, 주인님도 좋은 하루 보내세요.\n");
                tts.speak("네 주인님도 좋은 하루 보내세요.", TextToSpeech.QUEUE_FLUSH, null);
            }else{
                txt.append("[Siri] : 무슨 말인지 이해하지 못했어요.\n");
                tts.speak("무슨 말인지 이해하지 못했어요.", TextToSpeech.QUEUE_FLUSH, null);
            }
        }catch (Exception e){
            toast(e.toString());
        }
    }
    private RecognitionListener recognitionListener = new RecognitionListener() {
        @Override
        public void onReadyForSpeech(Bundle bundle) {
        }

        @Override
        public void onBeginningOfSpeech() {
        }

        @Override
        public void onRmsChanged(float v) {
        }

        @Override
        public void onBufferReceived(byte[] bytes) {
        }

        @Override
        public void onEndOfSpeech() {
        }

        @Override
        public void onError(int i) {
            textView.setText("다시 말씀해주세요.");
        }

        @Override
        public void onResults(Bundle bundle) {
            String key = "";
            key = SpeechRecognizer.RESULTS_RECOGNITION;
            ArrayList<String> mResult = bundle.getStringArrayList(key);

            String[] rs = new String[mResult.size()];
            mResult.toArray(rs);

            textView.setText("[나] : "+rs[0]+"\n");
            replyAnswer(rs[0], textView);
        }

        @Override
        public void onPartialResults(Bundle bundle) {
        }

        @Override
        public void onEvent(int i, Bundle bundle) {
        }
    };

    private void toast(String msg){
        Toast.makeText(this, msg, Toast.LENGTH_LONG).show();
    }


    public interface onKeyBackPressedListener {
        public void onBack();
    }

    private onKeyBackPressedListener mOnKeyBackPressedListener;

    public void setOnKeyBackPressedListener(onKeyBackPressedListener listener) {
        mOnKeyBackPressedListener = listener;
    } // In MyActivity
}