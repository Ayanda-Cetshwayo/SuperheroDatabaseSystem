# One Kick Heroes Academy - Superhero Database System

# Welcome to One Kick Heroes Academy - Superhero Database System

This is a professional Windows Forms application created for One Kick Heroes Academy to manage and organise superhero trainee records. The system was built to replace an outdated paper-based examination recording system that caused delays in processing trainee assessments and rank assignments.

# Project Overview

The One Kick Heroes Academy Superhero Database System is an advanced Windows Forms application designed to streamline superhero record management and examination data. It provides real-time rank calculation, comprehensive analytics, and seamless data storage. The system automatically evaluates superhero exam scores and assigns appropriate ranks and threat levels based on academy standards.

# The Problem This Application Solves

Previously, One Kick Heroes Academy relied on manual, paper-based systems to record superhero trainee examination results. This process was inefficient, error-prone, and created significant bottlenecks during peak examination periods. Assessors struggled to keep up with the volume of records. The application now automates this entire workflow, providing instant rank calculation and centralised digital record management.

# How the Application Works

# Core Functionality

Hero Registration System
The application provides a comprehensive hero registry where administrators can add new superhero trainees to the academy database. Each hero record includes essential information: Hero ID, name, age, superpower classification, and examination score. The interface is user-friendly with real-time validation to prevent data entry errors.

Automatic Rank Calculation
When an examination score is entered, the system automatically calculates the appropriate rank based on academy standards. As users type the exam score, the rank and threat level update in real-time. This eliminates manual calculations and ensures all records are consistent. A progress bar provides visual feedback of the score entered.

Real-Time Analytics Dashboard
The analytics panel displays live statistics about the entire hero database. This includes the total number of superheroes registered, average age across all heroes, and average examination score. The rank distribution section shows how many heroes fall into each rank category, providing valuable insights into overall academy performance.

Comprehensive Data Management
The application supports full CRUD operations: Create (add heroes), Read (view all heroes in the grid), Update (modify existing records), and Delete (remove heroes with confirmation). The data grid displays all heroes with sortable columns showing ID, name, age, superpower, exam score, rank, and threat level.

Report Generation
The application can generate detailed summary reports that include total hero count, average statistics, and rank distribution analysis. These reports are automatically saved to a text file for record-keeping and administrative analysis.

# Features

- ✓ Add superheroes with automatic rank calculation based on exam scores
- ✓ Update hero records with real-time score validation and rank recalculation
- ✓ Delete heroes with confirmation dialogs to prevent accidental data loss
- ✓ View all heroes in an organised, sortable grid with all key information
- ✓ Real-time analytics and statistics for the entire hero database
- ✓ Generate comprehensive reports with rank distribution analysis
- ✓ Score progress indicator showing visual representation of exam performance
- ✓ Rank distribution visualisation with colour-coded rank tiers
- ✓ Input validation for all fields with helpful error messages
- ✓ Professional dark theme with superhero-inspired design
- ✓ Persistent data storage using file-based system
- ✓ Git version control integration for project tracking

# Ranking System

The academy uses a four-tier ranking system to classify superhero trainees based on examination performance. Each rank corresponds to a threat level that indicates the level of danger the hero is prepared to handle within the academy environment.

S-Rank (Exam Score: 81-100)
S-Rank is the highest classification for superhero trainees. Heroes with S-Rank are fully qualified and capable of handling Finals Week level threats, which pose risks to the entire academy. These trainees demonstrate mastery of their abilities.

A-Rank (Exam Score: 61-80)
A-Rank heroes are well-trained and competent. They can manage Midterm Madness level threats, which affect department-wide operations and multiple study groups. A-Rank represents solid performance with room for improvement.

B-Rank (Exam Score: 41-60)
B-Rank heroes have foundational skills and moderate competency. They manage Group Project Gone Wrong level threats, which affect smaller groups and individual study sessions. B-Rank trainees show promise but require additional training.

C-Rank (Exam Score: 0-40)
C-Rank is the entry-level classification for trainees showing basic competency. C-Rank heroes handle Pop Quiz level threats, which present potential risk to individual students. This rank indicates trainees who need significant development before advancement.

# Technologies Used

Programming Language
C# - A modern, object-oriented programming language providing type safety and extensive framework support.

User Interface Framework
Windows Forms - The native .NET framework for creating Windows desktop applications with rich UI components.

.NET Framework
.NET Framework 4.7.2 or .NET 8.0+ - Provides the runtime environment and class libraries necessary for application execution.

Data Storage
File-based data storage using pipe-delimited text files for simplicity and readability. Data is organised in a structured format that is easy to parse and verify.

Version Control
Git - A distributed version control system for tracking project changes and managing collaboration.

Repository Hosting
GitHub - A cloud-based platform for hosting the Git repository and enabling team collaboration.

# System Requirements

Software Requirements
- Visual Studio 2022 or later (Community Edition available)
- .NET Framework 4.7.2 or .NET 8.0+
- Windows Operating System (Windows 10 or later recommended)
- Git for Windows (for version control)

Hardware Requirements
- Minimum 4GB RAM
- 500MB available disk space
- Standard processor (any modern CPU)
- Display resolution of at least 1400x900 pixels recommended

# How to Run

Step 1: Clone the Repository
Open Command Prompt or PowerShell and run this command to clone the repository:
```
git clone https://github.com/Ayanda-Cetshwayo/SuperheroDatabaseSystem.git
cd SuperheroDatabaseSystem
```

Step 2: Open in Visual Studio
Launch Visual Studio 2022 and open the `SuperheroDatabaseSystem.sln` solution file from the cloned repository folder.

Step 3: Build the Project
Press `Ctrl + B` to build the solution. Ensure there are no build errors. The solution should compile successfully.

Step 4: Run the Application
Press `Ctrl + F5` to run the application. The application window will open with the superhero database interface ready for use.

Step 5: Begin Adding Heroes
Use the left panel form to enter hero information, click the Add button, and watch as the hero appears in the database grid. Use the analytics panel to view statistics and generate reports.

# Installation

Prerequisites
- Visual Studio 2022 or later (free Community Edition available)
- .NET Framework 4.7.2 or .NET 8.0+
- Windows Operating System

Installation Steps
1. Download and install Visual Studio 2022 from https://visualstudio.microsoft.com
2. Install Git for Windows from https://git-scm.com
3. Clone this repository using the command above
4. Open the solution file in Visual Studio
5. Restore any NuGet packages if prompted
6. Build and run the application

# Project Structure

The project uses a standard Windows Forms application structure with clean separation of concerns. The `Program.cs` file contains the main application logic:

- `MainForm` class: Primary application form managing UI and user interactions
- `Superhero` class: Data model representing individual hero records
- `HeroTheme` class: Colour palette and theme definitions for professional styling
- Helper methods for CRUD operations, validation, file input/output, and calculations

Data is stored using two text files:
- `superheroes.txt`: Stores all hero records in pipe-delimited format
- `summary.txt`: Contains generated reports with statistics

# Version Control

The project uses Git for version control with meaningful commit messages tracking development progress. The repository is hosted on GitHub with public access for collaboration and reference.

# Commit History

The development process involved strategic commits at key milestones:
1. Initial project setup with application structure
2. Implementation of hero registry with automatic rank calculation
3. Addition of delete functionality and hero database grid
4. Completion of analytics dashboard and report generation

# Group Members

- Ayanda Cetshwayo
- Luyanda Cetshwayo

# Contact and Support

For questions, issues, or suggestions regarding this project, please contact the development team or visit the GitHub repository.
Development team : Ayanda Cetshwayo - 601466@student.belgiumcampus.ac.za OR Luyanda Cetshwayo - 601467@student.belgiumcampus.ac.za

# Licence and Academic Use

This project was developed as part of the PRG2782 Programming course at Belgium Campus. The code is provided for educational purposes and may be referenced or modified for academic study.

# Acknowledgements

This application was created to address a real operational need at One Kick Heroes Academy. It transforms a paper-based manual system into a modern, efficient digital solution. The development process emphasised clean code, user experience, and practical functionality.
