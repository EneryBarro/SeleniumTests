﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by Reqnroll (https://www.reqnroll.net/).
//      Reqnroll Version:2.0.0.0
//      Reqnroll Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Tests
{
    using Reqnroll;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "2.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("User login")]
    [NUnit.Framework.FixtureLifeCycleAttribute(NUnit.Framework.LifeCycle.InstancePerTestCase)]
    public partial class UserLoginFeature
    {
        
        private global::Reqnroll.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private static global::Reqnroll.FeatureInfo featureInfo = new global::Reqnroll.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "", "User login", "  As a user\r\n  I want to log in to the system\r\n  So that I can access my account", global::Reqnroll.ProgrammingLanguage.CSharp, featureTags);
        
#line 1 "Login.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public static async System.Threading.Tasks.Task FeatureSetupAsync()
        {
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public static async System.Threading.Tasks.Task FeatureTearDownAsync()
        {
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public async System.Threading.Tasks.Task TestInitializeAsync()
        {
            testRunner = global::Reqnroll.TestRunnerManager.GetTestRunnerForAssembly(featureHint: featureInfo);
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Equals(featureInfo) == false)))
            {
                await testRunner.OnFeatureEndAsync();
            }
            if ((testRunner.FeatureContext == null))
            {
                await testRunner.OnFeatureStartAsync(featureInfo);
            }
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public async System.Threading.Tasks.Task TestTearDownAsync()
        {
            await testRunner.OnScenarioEndAsync();
            global::Reqnroll.TestRunnerManager.ReleaseTestRunner(testRunner);
        }
        
        public void ScenarioInitialize(global::Reqnroll.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public async System.Threading.Tasks.Task ScenarioStartAsync()
        {
            await testRunner.OnScenarioStartAsync();
        }
        
        public async System.Threading.Tasks.Task ScenarioCleanupAsync()
        {
            await testRunner.CollectScenarioErrorsAsync();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Login attempt with different input clearing")]
        [NUnit.Framework.TestCaseAttribute("chrome", "standard_user", "secret_sauce", "clear both input fields", "I should see the error message \"Username is required\"", null)]
        [NUnit.Framework.TestCaseAttribute("chrome", "standard_user", "secret_sauce", "clear only password field", "I should see the error message \"Password is required\"", null)]
        [NUnit.Framework.TestCaseAttribute("chrome", "standard_user", "secret_sauce", "do not clear fields", "I should be redirected to the \"Swag Labs\" page", null)]
        [NUnit.Framework.TestCaseAttribute("edge", "standard_user", "secret_sauce", "clear both input fields", "I should see the error message \"Username is required\"", null)]
        [NUnit.Framework.TestCaseAttribute("edge", "standard_user", "secret_sauce", "clear only password field", "I should see the error message \"Password is required\"", null)]
        [NUnit.Framework.TestCaseAttribute("edge", "standard_user", "secret_sauce", "do not clear fields", "I should be redirected to the \"Swag Labs\" page", null)]
        [NUnit.Framework.TestCaseAttribute("firefox", "standard_user", "secret_sauce", "clear both input fields", "I should see the error message \"Username is required\"", null)]
        [NUnit.Framework.TestCaseAttribute("firefox", "standard_user", "secret_sauce", "clear only password field", "I should see the error message \"Password is required\"", null)]
        [NUnit.Framework.TestCaseAttribute("firefox", "standard_user", "secret_sauce", "do not clear fields", "I should be redirected to the \"Swag Labs\" page", null)]
        public async System.Threading.Tasks.Task LoginAttemptWithDifferentInputClearing(string browser, string username, string password, string clear_Action, string result, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("browser", browser);
            argumentsOfScenario.Add("username", username);
            argumentsOfScenario.Add("password", password);
            argumentsOfScenario.Add("clear_action", clear_Action);
            argumentsOfScenario.Add("result", result);
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Login attempt with different input clearing", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 6
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 7
    await testRunner.GivenAsync(string.Format("I navigate to the login page with \"{0}\"", browser), ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 8
    await testRunner.WhenAsync(string.Format("I enter username \"{0}\" and password \"{1}\"", username, password), ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 9
    await testRunner.AndAsync(string.Format("I perform \"{0}\" on input fields", clear_Action), ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 10
    await testRunner.AndAsync("I click the login button", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 11
    await testRunner.ThenAsync(string.Format("{0}", result), ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
    }
}
#pragma warning restore
#endregion
