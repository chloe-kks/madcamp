package com.frag.q.frag;

import java.util.ArrayList;
import java.util.List;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.database.Cursor;
import android.net.Uri;

import android.os.Bundle;
import android.provider.ContactsContract;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;

import android.widget.ListView;

import android.widget.TextView;

public class ContactListActivity extends Activity {
	private ListView lv_contactlist;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_nick_name);
		lv_contactlist = (ListView) findViewById(R.id.lv_contactlist);

	}

	@Override
	protected void onResume() {
		super.onResume();


		ContactsAdapter adapter = new ContactsAdapter(ContactListActivity.this,
				R.layout.list_item, getContactList());

		lv_contactlist.setAdapter(adapter);
		lv_contactlist
				.setOnItemClickListener(new AdapterView.OnItemClickListener() {

					@Override
					public void onItemClick(AdapterView<?> contactlist, View v,
											int position, long resid) {
						Contact phonenumber = (Contact) contactlist
								.getItemAtPosition(position);

						if (phonenumber == null) {
							return;
						}

						Intent data = new Intent();
						data.putExtra(com.frag.q.frag.NickNameActivity.SELECTED_PHONE, phonenumber
								.getPhonenum().replaceAll("-", ""));

						setResult(NickNameActivity.SUCCESS, data);
						finish();
					}
				});

	}

	private ArrayList<Contact> getContactList() {

		Uri uri = ContactsContract.CommonDataKinds.Phone.CONTENT_URI;

		String[] projection = new String[] {
				ContactsContract.CommonDataKinds.Phone.CONTACT_ID,
				ContactsContract.CommonDataKinds.Phone.NUMBER,
				ContactsContract.CommonDataKinds.Phone.DISPLAY_NAME };

		String[] selectionArgs = null;

		String sortOrder = ContactsContract.CommonDataKinds.Phone.DISPLAY_NAME
				+ " COLLATE LOCALIZED ASC";

		Cursor contactCursor = managedQuery(uri, projection, null,
				selectionArgs, sortOrder);

		ArrayList<Contact> contactlist = new ArrayList<Contact>();

		if (contactCursor.moveToFirst()) {
			do {
				String phonenumber = contactCursor.getString(1).replaceAll("-",
						"");
				if (phonenumber.length() == 10) {
					phonenumber = phonenumber.substring(0, 3) + "-"
							+ phonenumber.substring(3, 6) + "-"
							+ phonenumber.substring(6);
				} else if (phonenumber.length() > 8) {
					phonenumber = phonenumber.substring(0, 3) + "-"
							+ phonenumber.substring(3, 7) + "-"
							+ phonenumber.substring(7);
				}

				Contact acontact = new Contact();
				acontact.setPhotoid(contactCursor.getLong(0));
				acontact.setPhonenum(phonenumber);
				acontact.setName(contactCursor.getString(2));

				contactlist.add(acontact);
			} while (contactCursor.moveToNext());
		}

		return contactlist;

	}

	private class ContactsAdapter extends ArrayAdapter<Contact> {

		private int resId;
		private ArrayList<Contact> contactlist;
		private LayoutInflater Inflater;
		private Context context;

		public ContactsAdapter(Context context, int textViewResourceId,
							   List<Contact> objects) {
			super(context, textViewResourceId, objects);
			this.context = context;
			resId = textViewResourceId;
			contactlist = (ArrayList<Contact>) objects;
			Inflater = (LayoutInflater) ((Activity) context)
					.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		}

		@Override
		public View getView(int position, View v, ViewGroup parent) {
			ViewHolder holder;
			if (v == null) {
				v = Inflater.inflate(resId, null);
				holder = new ViewHolder();
				holder.tv_name = (TextView) v.findViewById(R.id.list_title);
				holder.tv_phonenumber = (TextView) v
						.findViewById(R.id.list_num);
				v.setTag(holder);
			} else {
				holder = (ViewHolder) v.getTag();
			}

			Contact acontact = contactlist.get(position);

			if (acontact != null) {
				holder.tv_name.setText(acontact.getName());
				holder.tv_phonenumber.setText(acontact.getPhonenum());

			}
			return v;
		}

		private class ViewHolder {
			TextView tv_name;
			TextView tv_phonenumber;
		}

	}

}
