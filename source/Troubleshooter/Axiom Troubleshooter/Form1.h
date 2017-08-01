#pragma once
#include <iostream>
#include <windows.h>
#include <string>
#include <sys/types.h>
#include <sys/stat.h>
//#pragma comment(lib, "shell32")
//#include <shellapi.h>
#include <stdlib.h>
#include <cstdlib>
#include <stdio.h>
#include <shlobj.h>
#pragma warning(disable:4996)
#ifdef _MSC_VER
#define _CRT_SECURE_NO_WARNINGS
#endif

namespace AxiomTroubleshooter {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace std;

	/// <summary>
	/// Summary for Main Form1
	/// </summary>
	public ref class Form1 : public System::Windows::Forms::Form
	{
	public:
		Form1(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~Form1()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Button^  btnOpen;
	protected:
	private: System::Windows::Forms::Label^  labelInfo;
	private: System::Windows::Forms::Label^  label1;
	private: System::Windows::Forms::Label^  label2;
	private: System::Windows::Forms::Button^  btnDelete;
	private: System::Windows::Forms::Button^  btnDotNet;

	protected:

	protected:



	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			System::ComponentModel::ComponentResourceManager^  resources = (gcnew System::ComponentModel::ComponentResourceManager(Form1::typeid));
			this->btnOpen = (gcnew System::Windows::Forms::Button());
			this->labelInfo = (gcnew System::Windows::Forms::Label());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->btnDelete = (gcnew System::Windows::Forms::Button());
			this->btnDotNet = (gcnew System::Windows::Forms::Button());
			this->SuspendLayout();
			// 
			// btnOpen
			// 
			this->btnOpen->Location = System::Drawing::Point(34, 127);
			this->btnOpen->Name = L"btnOpen";
			this->btnOpen->Size = System::Drawing::Size(75, 23);
			this->btnOpen->TabIndex = 0;
			this->btnOpen->Text = L"Open";
			this->btnOpen->UseVisualStyleBackColor = true;
			this->btnOpen->Click += gcnew System::EventHandler(this, &Form1::btnOpen_Click);
			// 
			// labelInfo
			// 
			this->labelInfo->AutoSize = true;
			this->labelInfo->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 12, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(0)));
			this->labelInfo->Location = System::Drawing::Point(83, 15);
			this->labelInfo->Name = L"labelInfo";
			this->labelInfo->Size = System::Drawing::Size(139, 20);
			this->labelInfo->TabIndex = 1;
			this->labelInfo->Text = L"Fix Program Crash";
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 9.75F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(0)));
			this->label1->Location = System::Drawing::Point(43, 47);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(218, 32);
			this->label1->TabIndex = 2;
			this->label1->Text = L"Delete Axiom Last Save Settings in:\n%UserProfile%\\AppData\\Local\\";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 9.75F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(0)));
			this->label2->Location = System::Drawing::Point(39, 89);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(226, 16);
			this->label2->TabIndex = 3;
			this->label2->Text = L"Upgrade Microsoft .NET Framework.";
			// 
			// btnDelete
			// 
			this->btnDelete->Location = System::Drawing::Point(115, 127);
			this->btnDelete->Name = L"btnDelete";
			this->btnDelete->Size = System::Drawing::Size(75, 23);
			this->btnDelete->TabIndex = 4;
			this->btnDelete->Text = L"Delete";
			this->btnDelete->UseVisualStyleBackColor = true;
			this->btnDelete->Click += gcnew System::EventHandler(this, &Form1::btnDelete_Click);
			// 
			// btnDotNet
			// 
			this->btnDotNet->Location = System::Drawing::Point(196, 127);
			this->btnDotNet->Name = L"btnDotNet";
			this->btnDotNet->Size = System::Drawing::Size(75, 23);
			this->btnDotNet->TabIndex = 5;
			this->btnDotNet->Text = L".NET 4.5";
			this->btnDotNet->UseVisualStyleBackColor = true;
			this->btnDotNet->Click += gcnew System::EventHandler(this, &Form1::btnDotNet_Click);
			// 
			// Form1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(304, 171);
			this->Controls->Add(this->btnDotNet);
			this->Controls->Add(this->btnDelete);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->labelInfo);
			this->Controls->Add(this->btnOpen);
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedSingle;
			this->Icon = (cli::safe_cast<System::Drawing::Icon^>(resources->GetObject(L"$this.Icon")));
			this->MaximizeBox = false;
			this->Name = L"Form1";
			this->StartPosition = System::Windows::Forms::FormStartPosition::CenterScreen;
			this->Text = L"Axiom Troubleshooter";
			this->Load += gcnew System::EventHandler(this, &Form1::Form1_Load);
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion

		// Form Load
	private: System::Void Form1_Load(System::Object^  sender, System::EventArgs^  e) {
	}

			 // Open Button
	private: System::Void btnOpen_Click(System::Object^  sender, System::EventArgs^  e) {

		//const char* userprofile = getenv("USERPROFILE");
		//if ( userprofile == NULL ) {
		//	//  Big problem...
		//} else {
		//	string s( userprofile );
		//}

		// Get %userprofile% from Environment Variables
		char* userProfilePath = getenv("USERPROFILE");

		// AppData Local Path
		string appDataPath = "\\AppData\\Local";

		// Combine Paths
		string fullPathStr = string(userProfilePath) + string(appDataPath);
		// Execute Explorer + FullPath
		string execPathStr = string("explorer ") + string(fullPathStr);

		// Convert String to Const Char
		const char* fullPath = fullPathStr.c_str();
		const char* execPath = execPathStr.c_str();

		// Open User Profile Directory in Window
		struct stat info;

		// cannot access
		if (stat(fullPath, &info) != 0)
		{
			MessageBox::Show("Could Not Access Folder.");
		}
		// dir exists
		else if (info.st_mode & S_IFDIR)
		{
			system(execPath); // Open Window
		}
		// dir does not exist
		else
		{
			MessageBox::Show("Could Not Access Folder.");
		}
	}


			 // Delete
	private: System::Void btnDelete_Click(System::Object^  sender, System::EventArgs^  e) {

		// Get %userprofile% from Environment Variables
		char* userProfilePath = getenv("USERPROFILE");

		// AppData Local Path
		string settingsPath = "\\AppData\\Local\\Axiom";

		// Combine Paths
		string fullPathStr = string(userProfilePath) + string(settingsPath);
		// Execute CMD Remove + FullPath
		string execRemoveStr = string("RD /S /Q ") + string(fullPathStr);

		// Convert String to Const Char
		const char* fullPath = fullPathStr.c_str();
		const char* execRemove = execRemoveStr.c_str();


		// Convert fullPathStr to a System:string
		String^ msg = gcnew System::String(fullPathStr.c_str());
		// Open Yes No Dialog Box
		//MessageBox::Show(msg, "Delete Confirm", MessageBoxButtons::YesNo);

		// Open Yes No Dialog Box
		if (MessageBox::Show(msg, "Delete Confirm", MessageBoxButtons::YesNo) == ::System::Windows::Forms::DialogResult::Yes)
		{
			// Delete
			struct stat info;

			// cannot access
			if (stat(fullPath, &info) != 0)
			{
				MessageBox::Show("No Previous Settings Found.");
			}
			// dir exists
			else if (info.st_mode & S_IFDIR)
			{
				system(execRemove); // Remove Setting Folder
			}
			// dir does not exist
			else
			{
				MessageBox::Show("No Previous Settings Found.");
			}

		}
		// If No
		else
		{
			// Stop
		}
	}


			 // .NET Upgrade
	private: System::Void btnDotNet_Click(System::Object^  sender, System::EventArgs^  e) {
		system("start https://www.microsoft.com/en-us/download/details.aspx?id=30653");
	}


	};

}

