#pragma once
#include <iostream>
#include <windows.h>
#include <string>
#include <sys/types.h>
#include <sys/stat.h>
#pragma comment(lib, "shell32")
#include <shellapi.h>
#include <stdlib.h>
#include <cstdlib>
#include <stdio.h>
#include <shlobj.h>
#include <fstream>
#include <iostream>
#include <cstdio>
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
	/// Summary for Form1
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
	private: System::Windows::Forms::Label^  label1;
	protected: 
	private: System::Windows::Forms::Label^  label2;
	private: System::Windows::Forms::Button^  btnOpen;
	private: System::Windows::Forms::Button^  bntDelete;
	private: System::Windows::Forms::Button^  btndotNET;



	private: System::Windows::Forms::Label^  label3;

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
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->btnOpen = (gcnew System::Windows::Forms::Button());
			this->bntDelete = (gcnew System::Windows::Forms::Button());
			this->btndotNET = (gcnew System::Windows::Forms::Button());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->SuspendLayout();
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 12, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(0)));
			this->label1->Location = System::Drawing::Point(76, 19);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(139, 20);
			this->label1->TabIndex = 0;
			this->label1->Text = L"Fix Program Crash";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 9.75F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(0)));
			this->label2->Location = System::Drawing::Point(17, 57);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(262, 16);
			this->label2->TabIndex = 1;
			this->label2->Text = L"Delete axiom.conf file in %LocalAppData%";
			// 
			// btnOpen
			// 
			this->btnOpen->Location = System::Drawing::Point(28, 120);
			this->btnOpen->Name = L"btnOpen";
			this->btnOpen->Size = System::Drawing::Size(75, 23);
			this->btnOpen->TabIndex = 2;
			this->btnOpen->Text = L"Open";
			this->btnOpen->UseVisualStyleBackColor = true;
			this->btnOpen->Click += gcnew System::EventHandler(this, &Form1::btnOpen_Click);
			// 
			// bntDelete
			// 
			this->bntDelete->Location = System::Drawing::Point(109, 120);
			this->bntDelete->Name = L"bntDelete";
			this->bntDelete->Size = System::Drawing::Size(75, 23);
			this->bntDelete->TabIndex = 3;
			this->bntDelete->Text = L"Delete";
			this->bntDelete->UseVisualStyleBackColor = true;
			this->bntDelete->Click += gcnew System::EventHandler(this, &Form1::bntDelete_Click);
			// 
			// btndotNET
			// 
			this->btndotNET->Location = System::Drawing::Point(190, 120);
			this->btndotNET->Name = L"btndotNET";
			this->btndotNET->Size = System::Drawing::Size(75, 23);
			this->btndotNET->TabIndex = 4;
			this->btndotNET->Text = L".NET 4.5";
			this->btndotNET->UseVisualStyleBackColor = true;
			this->btndotNET->Click += gcnew System::EventHandler(this, &Form1::btndotNET_Click);
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 9.75F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(0)));
			this->label3->Location = System::Drawing::Point(34, 81);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(226, 16);
			this->label3->TabIndex = 5;
			this->label3->Text = L"Upgrade Microsoft .NET Framework.";
			// 
			// Form1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(294, 161);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->btndotNET);
			this->Controls->Add(this->bntDelete);
			this->Controls->Add(this->btnOpen);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->label1);
			this->Icon = (cli::safe_cast<System::Drawing::Icon^  >(resources->GetObject(L"$this.Icon")));
			this->MaximizeBox = false;
			this->MaximumSize = System::Drawing::Size(310, 200);
			this->MinimumSize = System::Drawing::Size(310, 200);
			this->Name = L"Form1";
			this->StartPosition = System::Windows::Forms::FormStartPosition::CenterScreen;
			this->Text = L"Axiom Troubleshooter";
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion

	// Open Button
	private: System::Void btnOpen_Click(System::Object^  sender, System::EventArgs^  e) 
	{
		// Get %userprofile% from Environment Variables
		char* userProfilePath = getenv("USERPROFILE");

		// AppData Local Path
		string appDataPath = "\\AppData\\Local";

		// Combine Paths
		string fullPathStr = string(userProfilePath) + string(appDataPath) + string("\\Axiom UI");

		// Execute Explorer + FullPath
		string execPathStr = string("explorer ") + string("\"") + string(fullPathStr) + string("\"");

		// Convert String to Const Char
		const char* fullPath = fullPathStr.c_str(); 
		const char* execPath = execPathStr.c_str(); 

		// Open User Profile Directory in Window
		struct stat info;

		// Can't Acces Path
		if( stat( fullPath, &info ) != 0 ) 
		{
			MessageBox::Show("Could not access directory.");
		}
		// Path Exists
		else if( info.st_mode & S_IFDIR ) 
		{
			// open window
			system(execPath); 
		}
		// Path does not exist
		else 
		{
			MessageBox::Show("Directory does not exist."); 
		}
	}


	// Delete Button
	private: System::Void bntDelete_Click(System::Object^  sender, System::EventArgs^  e) 
	{
		// Get %userprofile% from Environment Variables
		char* userProfilePath = getenv("USERPROFILE");

		// AppData Local Path
		string settingsPath = "\\AppData\\Local\\Axiom UI";

		// Combine Paths
		string fullPathStr = string(userProfilePath) + string(settingsPath) + string("\\axiom.conf");

		// Execute CMD Remove + FullPath
		string execRemoveStr = string(fullPathStr);

		// Convert String to Const Char
		const char* fullPath = fullPathStr.c_str(); 
		const char* execDelete = execRemoveStr.c_str(); 

		// Convert fullPathStr to a System:string
		String^ msg = gcnew System::String(fullPathStr.c_str());

		// Open Yes No Dialog Box
		if (MessageBox::Show(msg, "Delete Confirm", MessageBoxButtons::YesNo) == ::System::Windows::Forms::DialogResult::Yes) 
		{					
			// Delete
			struct stat info;

			// Check if file exists
			ifstream file(fullPath);

			// Exists
			if (file) 
			{
				// Close the file before deleting
				file.close();

				// Delete axiom.conf
				std::remove(execDelete);
			}

			// Does not exist
			else 
			{
				MessageBox::Show("Config file not found.");
			}
		}		
	}
	

	// .NET Uprade
	private: System::Void btndotNET_Click(System::Object^  sender, System::EventArgs^  e) 
	{
		system("start https://www.microsoft.com/en-us/download/details.aspx?id=30653");
	}

};
}

