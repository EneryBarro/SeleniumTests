# Selenium Test Automation Project

This project contains automated tests for the login functionality of the **SauceDemo** website using Selenium WebDriver. The tests are parameterized, parallelized, and logged using NLog.

## Task Description

The goal of the project is to automate the login functionality and validate the expected behavior for different test cases:

- **UC-1**: Test login form with empty credentials.
- **UC-2**: Test login form with credentials by passing Username only.
- **UC-3**: Test login form with valid Username and Password.

The tests are performed using different browsers (Chrome and Edge) and use the following technologies:

- **Test Automation Tool**: Selenium WebDriver
- **Browsers**: Chrome, Edge
- **Locators**: XPath
- **Test Runner**: MSTest
- **Patterns**: Abstract Factory
- **Assertions**: Fluent Assertions
- **Logging**: NLog
- **Parallel Execution**: Configured in `.runsettings` file

## Prerequisites

To run the tests, ensure that the following tools are installed:

1. **Visual Studio** (2019 or later)
2. **.NET Framework 4.5 or later**
3. **Selenium WebDriver** and **FluentAssertions** packages from NuGet
4. **MSTest** test framework (via NuGet)
5. **NLog** for logging

## Setup

### 1. Clone the repository

Clone this repository to your local machine:

```bash
https://github.com/EneryBarro/SeleniumTests.git
```

### 2. Install NuGet Packages

Open the project in **Visual Studio**, and restore NuGet packages:

1. Right-click on the solution in the **Solution Explorer**.

2. Select **Manage NuGet Packages**.

Make sure the following packages are installed:

- Selenium.WebDriver

- FluentAssertions

- NLog

- Microsoft.NET.Test.Sdk

- MSTest.TestFramework

### 3. Configure the WebDriver

The project uses the **ChromeDriver** and **EdgeDriver** for testing. Ensure that the corresponding drivers are in your system’s PATH.

### 4. Run the Tests

To run the tests:

1. Open **Test Explorer** in Visual Studio.

2. Click **Run All** to run all tests or select individual tests to run.

## Logging

The project uses **NLog** for logging. The logs will be written to a log file located in the **logs/** directory. The following types of log messages are used:

- Info: General information about the test execution.

- Warn: Warnings or errors during test execution.
