# Driver License Management System (DVLD)

A C# Windows Forms application designed to manage **driving licenses, drivers, users, and related operations**.  
The system helps handle license issuance, detention, release, and tracking with an integrated database.  

---

## 🚀 Features  

### 🔑 License Services
- Issue a driving license for the first time (with license categories & validation).  
- Retake tests (vision, theory, and practical driving) if failed.  
- Renew expired driving licenses (with required checks).  
- Replace a lost license (issue new license after validation).  
- Replace a damaged license (with proper tracking).  
- Release a detained license (after paying fines).  
- Issue international driving licenses (valid only for eligible categories).  

---

## 📋 License Application Management
- Create and manage service requests with:  
  - Application number & date  
  - Applicant’s personal information (linked by National ID)  
  - Service type (first-time license, renewal, replacement, etc.)  
  - Application status (New, Canceled, Completed)  
  - Fees paid  
- Prevent duplicate or conflicting applications.  
- Ensure applicant eligibility based on **age, license class rules, and existing licenses**.  

---

## 🚗 License Categories (Classes)
Supports **7 categories**:  
1. Small motorcycles  
2. Heavy motorcycles  
3. Regular car license  
4. Commercial license (taxi/limousine)  
5. Agricultural vehicles  
6. Small/medium buses  
7. Trucks & heavy vehicles  

Each category has rules for:  
- Minimum age  
- Fees  
- Validity period  

✅ The system checks if an applicant meets requirements before issuing the license.  

---

## 🧾 Tests & Exams
- **Vision Test** (must pass before other exams).  
- **Theoretical Test** (traffic rules, safety, scored out of 100).  
- **Practical Driving Test** (based on license category).  

Each test includes:  
- Fee  
- Scheduled date  
- Result (Pass/Fail)  

📌 Applicants must pass tests **in sequence**.  
❌ Failed applicants can **reschedule** after paying fees again.  

---

## 👤 Person Management
- Manage all applicants and drivers in the system.  
- Prevent duplicate entries by National ID.  
- Store detailed info:  
  - National ID  
  - Full name  
  - Date of birth  
  - Address  
  - Phone number  
  - Email  
  - Nationality  
  - Profile photo  

---

## 👥 User Management (System Users)
- Add, edit, delete, or freeze user accounts.  
- Assign permissions/roles.  
- Link each system user to a person in the system.  
- User credentials: **username + password**.  

---

## 📑 Requests & Applications
- Search by application number or applicant’s National ID.  
- View all applications with status & fees.  
- Filter by status (New, Completed, Canceled).  
- Link requests to applicants and services.  

---

## 🧾 Tests Management
- Fixed test types: Vision, Theory, Practical.  
- Only **fees** are editable.  

---

## 🪪 License Management
- Issue licenses (new, renewal, replacement).  
- Manage detained licenses:  
  - Record detention details (reason, fine, date).  
  - Track release after fine payment.  
- Search licenses by **license number** or **National ID**.  
- Track all licenses historically (no overwriting).  

---

## ⚙️ System Administration
- Manage license categories (edit age, validity, fees).  
- Track all system actions with **user ID & timestamp**.  

---

## 🛠️ Built With
- **C# .NET (Windows Forms)**  
- **SQL Server** (for database management)  
- **ADO.NET** (for database connectivity)  
- **Visual Studio** (for development)  
- **3-Tier Architecture** (UI Layer, Business Logic Layer, Data Access Layer)  

---


## ⚙️ Setup & Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/IslamTaleb11/dvld-license-management.git

2. Open the solution in Visual Studio.

3. Configure the SQL Server connection string in the clsDataAccessLayerSettings class.

4. Build and run the project.
