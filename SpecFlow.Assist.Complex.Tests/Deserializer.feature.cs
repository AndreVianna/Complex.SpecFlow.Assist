﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace SpecFlow.Assist.Complex.Tests
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class DeserializerFeature : object, Xunit.IClassFixture<DeserializerFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Deserializer.feature"
#line hidden
        
        public DeserializerFeature(DeserializerFeature.FixtureData fixtureData, SpecFlow_Assist_Complex_Tests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "", "Deserializer", "Transforms table in complex types", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Complex class with vertical table")]
        [Xunit.TraitAttribute("FeatureTitle", "Deserializer")]
        [Xunit.TraitAttribute("Description", "Complex class with vertical table")]
        [Xunit.TraitAttribute("Category", "Deserializer")]
        public virtual void ComplexClassWithVerticalTable()
        {
            string[] tagsOfScenario = new string[] {
                    "Deserializer"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Complex class with vertical table", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 5
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "Field",
                            "Value"});
                table1.AddRow(new string[] {
                            "Id",
                            "\"CEFFAB59-D548-4E3A-B05E-5FDA43B4A4CF\""});
#line 6
 testRunner.When("I define a table like", ((string)(null)), table1, "When ");
#line hidden
#line 9
 testRunner.Then("the result object should not be null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 10
 testRunner.And("the \'Id\' property should be \'CEFFAB59-D548-4E3A-B05E-5FDA43B4A4CF\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Complex class with basic properties")]
        [Xunit.TraitAttribute("FeatureTitle", "Deserializer")]
        [Xunit.TraitAttribute("Description", "Complex class with basic properties")]
        public virtual void ComplexClassWithBasicProperties()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Complex class with basic properties", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 12
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "Field",
                            "Value"});
                table2.AddRow(new string[] {
                            "Id",
                            "\"1f576fa6-16c9-4905-95f8-e00cad6a8ded\""});
                table2.AddRow(new string[] {
                            "String",
                            "\"Some string.\""});
                table2.AddRow(new string[] {
                            "Integer",
                            "42"});
                table2.AddRow(new string[] {
                            "Decimal",
                            "3.141592"});
                table2.AddRow(new string[] {
                            "Boolean",
                            "True"});
                table2.AddRow(new string[] {
                            "DateTime",
                            "\"2020-02-20T12:34:56.789\""});
#line 13
 testRunner.When("I define a table like", ((string)(null)), table2, "When ");
#line hidden
#line 21
 testRunner.Then("the result object should not be null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 22
 testRunner.And("the \'Id\' property should be \'1F576FA6-16C9-4905-95F8-E00CAD6A8DED\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 23
 testRunner.And("the \'String\' property should be \'Some string.\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 24
 testRunner.And("the \'Integer\' property should be \'42\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 25
 testRunner.And("the \'Decimal\' property should be \'3.141592\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 26
 testRunner.And("the \'Boolean\' property should be \'True\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 27
 testRunner.And("the \'DateTime\' property should be \'2020-02-20T12:34:56.789\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Complex class with nullable properties")]
        [Xunit.TraitAttribute("FeatureTitle", "Deserializer")]
        [Xunit.TraitAttribute("Description", "Complex class with nullable properties")]
        public virtual void ComplexClassWithNullableProperties()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Complex class with nullable properties", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 29
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                            "Field",
                            "Value"});
                table3.AddRow(new string[] {
                            "Id",
                            "\"E01366F0-95FA-4372-AB7F-89F9DFBEF501\""});
                table3.AddRow(new string[] {
                            "String",
                            "null"});
                table3.AddRow(new string[] {
                            "Integer",
                            "NULL"});
                table3.AddRow(new string[] {
                            "Decimal",
                            "Null"});
                table3.AddRow(new string[] {
                            "Boolean",
                            "default"});
                table3.AddRow(new string[] {
                            "DateTime",
                            ""});
                table3.AddRow(new string[] {
                            "Complex",
                            "null"});
#line 30
 testRunner.When("I define a table like", ((string)(null)), table3, "When ");
#line hidden
#line 39
 testRunner.Then("the result object should not be null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 40
 testRunner.And("the \'Id\' property should be \'E01366F0-95FA-4372-AB7F-89F9DFBEF501\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 41
 testRunner.And("the \'String\' property should be null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 42
 testRunner.And("the \'Integer\' property should be null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 43
 testRunner.And("the \'Decimal\' property should be null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 44
 testRunner.And("the \'Boolean\' property should be null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 45
 testRunner.And("the \'DateTime\' property should be null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 46
 testRunner.And("the \'Complex\' property should be null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Complex class with collection properties")]
        [Xunit.TraitAttribute("FeatureTitle", "Deserializer")]
        [Xunit.TraitAttribute("Description", "Complex class with collection properties")]
        public virtual void ComplexClassWithCollectionProperties()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Complex class with collection properties", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 48
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                            "Field",
                            "Value"});
                table4.AddRow(new string[] {
                            "Id",
                            "\"92849268-2695-4726-AD10-486BD407BE64\""});
                table4.AddRow(new string[] {
                            "Lines[0]",
                            "\"Some line.\""});
                table4.AddRow(new string[] {
                            "Lines[1]",
                            "\"\""});
                table4.AddRow(new string[] {
                            "Lines[2]",
                            "\"Another line.\""});
                table4.AddRow(new string[] {
                            "Lines[3]",
                            "\"Last line.\""});
                table4.AddRow(new string[] {
                            "Numbers[0]",
                            "101"});
                table4.AddRow(new string[] {
                            "Numbers[1]",
                            "-201"});
                table4.AddRow(new string[] {
                            "Numbers[2]",
                            "0"});
#line 49
 testRunner.When("I define a table like", ((string)(null)), table4, "When ");
#line hidden
#line 59
 testRunner.Then("the result object should not be null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 60
 testRunner.And("the \'Id\' property should be \'92849268-2695-4726-AD10-486BD407BE64\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 61
 testRunner.And("the \'Lines\' property should have 4 items", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 62
 testRunner.And("the item 0 from \'Lines\' should be \'Some line.\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 63
 testRunner.And("the item 1 from \'Lines\' should be \'\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 64
 testRunner.And("the item 2 from \'Lines\' should be \'Another line.\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 65
 testRunner.And("the item 3 from \'Lines\' should be \'Last line.\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 66
 testRunner.And("the \'Numbers\' property should have 3 items", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 67
 testRunner.And("the item 0 from \'Numbers\' should be \'101\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 68
 testRunner.And("the item 1 from \'Numbers\' should be \'-201\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 69
 testRunner.And("the item 2 from \'Numbers\' should be \'0\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Complex class with complex properties")]
        [Xunit.TraitAttribute("FeatureTitle", "Deserializer")]
        [Xunit.TraitAttribute("Description", "Complex class with complex properties")]
        public virtual void ComplexClassWithComplexProperties()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Complex class with complex properties", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 71
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                            "Field",
                            "Value"});
                table5.AddRow(new string[] {
                            "Id",
                            "\"7AA4F67D-52A4-42FF-B4DE-57C50063314B\""});
                table5.AddRow(new string[] {
                            "Children[0].Id",
                            "\"9C4060F9-F949-4036-B82C-E16E7413351D\""});
                table5.AddRow(new string[] {
                            "Children[0].String",
                            "\"Some string.\""});
                table5.AddRow(new string[] {
                            "Children[0].Integer",
                            "42"});
                table5.AddRow(new string[] {
                            "Children[1].Id",
                            "\"C052E9F2-BA16-41A0-A30C-6A4B0DF9AF33\""});
                table5.AddRow(new string[] {
                            "Children[1].Decimal",
                            "3.141592"});
                table5.AddRow(new string[] {
                            "Children[1].Boolean",
                            "False"});
                table5.AddRow(new string[] {
                            "Children[1].DateTime",
                            "\"2020-02-20T12:34:56.789\""});
                table5.AddRow(new string[] {
                            "Complex.Id",
                            "\"782BCD4A-7BD2-455F-9841-52F368C8BAA1\""});
                table5.AddRow(new string[] {
                            "Complex.String",
                            "\"Some string.\""});
                table5.AddRow(new string[] {
                            "Complex.Integer",
                            "42"});
                table5.AddRow(new string[] {
                            "Complex.Decimal",
                            "3.141592"});
                table5.AddRow(new string[] {
                            "Complex.Boolean",
                            "True"});
                table5.AddRow(new string[] {
                            "Complex.DateTime",
                            "\"2020-02-20T12:34:56.789\""});
                table5.AddRow(new string[] {
                            "Complex.Complex.Id",
                            "\"2EDD7F54-9B0F-4F68-B090-41CE9F9D8402\""});
                table5.AddRow(new string[] {
                            "Complex.Complex.Complex.Id",
                            "\"201C8E6D-9AFE-4244-A21F-288BD1C0460B\""});
#line 72
 testRunner.When("I define a table like", ((string)(null)), table5, "When ");
#line hidden
#line 90
 testRunner.Then("the result object should not be null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 91
 testRunner.And("the \'Id\' property should be \'7AA4F67D-52A4-42FF-B4DE-57C50063314B\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 92
 testRunner.And("the \'Children\' property should have 2 items", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                            "Field",
                            "Value"});
                table6.AddRow(new string[] {
                            "Id",
                            "\"9C4060F9-F949-4036-B82C-E16E7413351D\""});
                table6.AddRow(new string[] {
                            "String",
                            "\"Some string.\""});
                table6.AddRow(new string[] {
                            "Integer",
                            "42"});
#line 93
 testRunner.And("the item 0 from \'Children\' should be", ((string)(null)), table6, "And ");
#line hidden
                TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                            "Field",
                            "Value"});
                table7.AddRow(new string[] {
                            "Id",
                            "\"C052E9F2-BA16-41A0-A30C-6A4B0DF9AF33\""});
                table7.AddRow(new string[] {
                            "Decimal",
                            "3.141592"});
                table7.AddRow(new string[] {
                            "Boolean",
                            "False"});
                table7.AddRow(new string[] {
                            "DateTime",
                            "\"2020-02-20T12:34:56.789\""});
#line 98
 testRunner.And("the item 1 from \'Children\' should be", ((string)(null)), table7, "And ");
#line hidden
                TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                            "Field",
                            "Value"});
                table8.AddRow(new string[] {
                            "Id",
                            "\"782BCD4A-7BD2-455F-9841-52F368C8BAA1\""});
                table8.AddRow(new string[] {
                            "String",
                            "\"Some string.\""});
                table8.AddRow(new string[] {
                            "Integer",
                            "42"});
                table8.AddRow(new string[] {
                            "Decimal",
                            "3.141592"});
                table8.AddRow(new string[] {
                            "Boolean",
                            "True"});
                table8.AddRow(new string[] {
                            "DateTime",
                            "\"2020-02-20T12:34:56.789\""});
                table8.AddRow(new string[] {
                            "Complex.Id",
                            "\"2EDD7F54-9B0F-4F68-B090-41CE9F9D8402\""});
                table8.AddRow(new string[] {
                            "Complex.Complex.Id",
                            "\"201C8E6D-9AFE-4244-A21F-288BD1C0460B\""});
#line 104
 testRunner.And("the \'Complex\' property should be", ((string)(null)), table8, "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Invalid property value")]
        [Xunit.TraitAttribute("FeatureTitle", "Deserializer")]
        [Xunit.TraitAttribute("Description", "Invalid property value")]
        public virtual void InvalidPropertyValue()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Invalid property value", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 115
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                            "Field",
                            "Value"});
                table9.AddRow(new string[] {
                            "Id",
                            "{Invalide Value Format}"});
#line 116
 testRunner.When("I define an invalid table like", ((string)(null)), table9, "When ");
#line hidden
#line 119
 testRunner.Then("the process should throw InvalidCastException for property \'Id\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                DeserializerFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                DeserializerFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
