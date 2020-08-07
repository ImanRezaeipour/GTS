using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Reflection;
using System.CodeDom;
using System.Web;
using System.Security;
using System.Security.Policy;
using System.Security.Permissions;
using System.Globalization;
using Microsoft.CSharp;
using GTS.Clock.Infrastructure.CompilerFramework;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Repository;
using log4net;

namespace GTS.Clock.Business.Compiler
{
    public class Compiler
    {
        #region Variables

        private enum CompileMood{Rule=0,Concept=1,Merge=2}
        private string referencePath = "";
        private string replacePath = "";
        private string backupDir = "";

              
        private string calculateDLLOutput = "";       
       
        private string rulesharpCodePath = "";
        private string cnpcsharpCodePath = "";
        private string mergedcsharpCodePath = "";
              
        private RuleRepository ruleRepository = new RuleRepository(false);
        private SecondaryConceptRepository scnCnpRepository = new SecondaryConceptRepository(false);
        private IList<Rule> _ruleList = new List<Rule>();
        private IList<SecondaryConcept> _cnpList = new List<SecondaryConcept>();
        private GTSLogger logger = new GTSLogger();
        CompilerConfig compilerConfig = new CompilerConfig();

        #endregion
        
        public Compiler() 
        {           
            referencePath= compilerConfig.ReadReferencePath();          
            rulesharpCodePath = compilerConfig.ReadRuleCSharpCodeOutputPath();
            cnpcsharpCodePath = compilerConfig.ReadConceptCSharpCodeOutputPath();
            mergedcsharpCodePath = compilerConfig.ReadMergedCSharpCodeOutputPath();
            calculateDLLOutput = compilerConfig.ReadDLLCSharpCodeOutputPath();
            replacePath = compilerConfig.ReadDLLReplacePath();
            backupDir = compilerConfig.ReadDLLBackupPath();
        }

        public void Run()
        {
            try
            {
                _ruleList = ruleRepository.GetAll();
                _cnpList = null;// scnCnpRepository.GetAll();

                string codePath = MergeCodeFiles();

                string dllPath = CompileFile(codePath, CompileMood.Merge);

                if (dllPath.Length > 0 && File.Exists(dllPath))
                {
                    BackupOldDLL(replacePath);
                    File.Copy(dllPath, replacePath, true);                                
                    logger.Logger.Info("GTS.Clock.Business.Calculator.dll was successfully compiled and replaced at:" + DateTime.Now.ToString());                                  
                }
                else
                {
                  logger.Logger.Error("GTS.Clock.Business.Calculator.dll Compiler Error at : " + DateTime.Now.ToString());
                }
            }
            catch (Exception ex)
            {
                logger.Logger.Error("GTS.Clock.Business.Calculator.dll Compiler Error : " + ex.Message);
            }
            finally 
            {
                _ruleList.Clear();
                _cnpList.Clear();
                logger.Flush();
            }
        }

        #region Private Methods
      
        /// <summary>
        /// کد مربوط به قوانین را تولید میکند
        /// </summary>
        /// <param name="exportToFile"></param>
        /// <returns>بسته به مقدار پارامتر ورودی یا آدرس فایل ذخیره سازی را بر میگرداند
        /// و یا کد تولید شده را برمیگرداند</returns>
        private string CreateRuleTextCodeFile(bool exportToFile,bool generateFullClassFile)
        {
            try
            {
                StringBuilder classBody = new StringBuilder();
                StringBuilder methods = new StringBuilder();
                if (generateFullClassFile)
                {
                    #region append namespace
                    classBody.AppendLine(compilerConfig.ReadConfigCSharp("namespace"));
                    #endregion
                }
                #region ClassDefinision
                classBody.AppendLine(compilerConfig.ReadConfigCSharp("RuleClassDefinision"));
                #endregion               
                #region Variables
                classBody.AppendLine(compilerConfig.ReadConfigCSharp("RuleVariables"));
                #endregion
                #region Constructors
                classBody.AppendLine(compilerConfig.ReadConfigCSharp("RuleConstructors"));
                #endregion
                #region Properties
                classBody.AppendLine(compilerConfig.ReadConfigCSharp("RuleProperties"));
                #endregion
                #region Methods
                classBody.AppendLine(compilerConfig.ReadConfigCSharp("RuleMethods"));
                #endregion
                #region Rules
                #region R Methods
                classBody.AppendLine("    #region Defined Methods");
                StringBuilder rMethod = new StringBuilder();
                foreach (Rule rule in _ruleList)
                {
                    rMethod.AppendFormat(@" public void R{0}(CategorisedRule MyRule) ", rule.IdentifierCode);
                    rMethod.AppendLine(@" { ");
                    throw new NotImplementedException();
                    //TODO: Add TranslatedCode to Rule
                    //rMethod.AppendLine(rule.TranslatedCode);
                    rMethod.AppendLine(@" } ");
                }
                classBody.AppendLine(rMethod.ToString());
                classBody.AppendLine("    #endregion");
                #endregion
                #endregion
                #region EndOfClass
                classBody.AppendLine(compilerConfig.ReadConfigCSharp("EndOfClass"));
                #endregion                
                #region EndOfNamespace
                    classBody.AppendLine(compilerConfig.ReadConfigCSharp("EndOfNamespace"));
                    #endregion
               

                if (exportToFile)
                {
                    StreamWriter writer = new StreamWriter(rulesharpCodePath, false);
                    writer.Write(classBody.ToString());
                    writer.Flush();
                    writer.Close();
                    return rulesharpCodePath;
                }
                else 
                {
                    return classBody.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n\r  GTS.Clock.Infrustructure.Compiler.Compiler.CreateTextCodeFile ");
            }

        }

        /// <summary>
        /// کد مربوط به مفاهیم را تولید میکند
        /// </summary>
        /// <param name="exportToFile"></param>
        /// <returns>بسته به مقدار پارامتر ورودی یا آدرس فایل ذخیره سازی را بر میگرداند
        /// و یا کد تولید شده را برمیگرداند</returns>
        private string CreateConceptTextCodeFile(bool exportToFile, bool generateFullClassFile)
        {
            try
            {
                StringBuilder classBody = new StringBuilder();
                StringBuilder methods = new StringBuilder();
                if (generateFullClassFile)
                {
                    #region append namespace
                    classBody.AppendLine(compilerConfig.ReadConfigCSharp("namespace"));
                    #endregion
                }
                #region ClassDefinision
                classBody.AppendLine(compilerConfig.ReadConfigCSharp("ConceptClassDefinision"));
                #endregion               
                #region Variables
                classBody.AppendLine(compilerConfig.ReadConfigCSharp("ConceptVariables"));
                #endregion
                #region Constructors
                classBody.AppendLine(compilerConfig.ReadConfigCSharp("ConceptConstructors"));
                #endregion
                #region Methods
                classBody.AppendLine(compilerConfig.ReadConfigCSharp("ConceptMethods"));
                #endregion
                #region C Methods
                classBody.AppendLine("    #region Methods");
                StringBuilder cMethod = new StringBuilder();
                foreach (SecondaryConcept cnp in _cnpList) 
                {
                    cMethod.AppendFormat(@" public void C{0}(BaseScndCnpValue Result, BaseScndCnp MyConcept) ", cnp.IdentifierCode);
                    cMethod.AppendLine(@" { ");
                    cMethod.AppendLine(cnp.Script);
                    cMethod.AppendLine(@" } ");
                }
                classBody.AppendLine(cMethod.ToString());
                classBody.AppendLine("    #endregion");
                #endregion
                #region EndOfClass
                classBody.AppendLine(compilerConfig.ReadConfigCSharp("EndOfClass"));
                #endregion
                #region EndOfNamespace
                classBody.AppendLine(compilerConfig.ReadConfigCSharp("EndOfNamespace"));
                #endregion

                if (exportToFile)
                {
                    StreamWriter writer = new StreamWriter(cnpcsharpCodePath, false);
                    writer.Write(classBody.ToString());
                    writer.Flush();
                    writer.Close();
                    return cnpcsharpCodePath;
                }
                else
                {
                    return classBody.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n\r  GTS.Clock.Infrustructure.Compiler.Compiler.CreateTextCodeFile ");
            }

        }

        private string MergeCodeFiles()
        {
            StringBuilder classBody = new StringBuilder();
            StreamWriter writer = new StreamWriter(mergedcsharpCodePath, false);

            #region append namespace & ObjectCalculator & RuleCalculatorFactory
            classBody.AppendLine(compilerConfig.ReadConfigCSharp("namespace"));
            classBody.AppendLine(compilerConfig.ReadConfigCSharp("ObjectCalculator"));
            classBody.AppendLine(compilerConfig.ReadConfigCSharp("RuleCalculatorFactory"));
            #endregion
            classBody.AppendLine(CreateRuleTextCodeFile(false, false));         
            classBody.AppendLine(CreateConceptTextCodeFile(false, false));           
            
            writer.Write(classBody.ToString());
            classBody = classBody.Remove(0, classBody.Length - 1);

            writer.Flush();
            writer.Close();

            return mergedcsharpCodePath;

        }
        
        private string CompileFile(string sourceFilePath,CompileMood compileMood)
        {
            try
            {
                if (File.Exists(sourceFilePath))
                {
                    Dictionary<string, string> options = new Dictionary<string, string> { { "CompilerVersion", "v3.5" } };


                    CSharpCodeProvider codeProvider = new CSharpCodeProvider(options);
                    ICodeCompiler icc = codeProvider.CreateCompiler();
                    //CodeSnippetCompileUnit 
                    CompilerParameters parameters = new CompilerParameters();
                    parameters.GenerateExecutable = true;
                   if (compileMood == CompileMood.Merge) 
                    {
                        parameters.OutputAssembly = calculateDLLOutput;
                    }

                    parameters.ReferencedAssemblies.Add("System.Core.dll");
                    parameters.ReferencedAssemblies.Add("System.Web.dll");
                    parameters.ReferencedAssemblies.Add(referencePath + "\\GTS.Clock.Model.dll");
                    parameters.ReferencedAssemblies.Add(referencePath + "\\GTS.Clock.Infrastructure.dll");                  
                    parameters.ReferencedAssemblies.Add(referencePath + "\\log4net.dll");
                    parameters.GenerateExecutable = false;                  
                    CompilerResults results = icc.CompileAssemblyFromFile(parameters, sourceFilePath);

                    string mood = "Rules";
                    if (compileMood == CompileMood.Concept) mood = "Concepts";
                    if (results.Errors.Count > 0)
                    {                       
                        foreach (CompilerError CompErr in results.Errors)
                        {
                            logger.Logger.ErrorFormat("Compile Error in {0}: {1}", mood, CompErr.ErrorText);
                        }
                        return "";
                    }
                    else 
                    {
                        logger.Logger.InfoFormat("Compile Success in {0}: {1}", mood, parameters.OutputAssembly);
                        return parameters.OutputAssembly;
                    }
                }
                else
                {
                    throw new Exception("File Not Found:  GTS.Clock.Infrustructure.Compiler.Compiler.CompileFile ");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n\r  GTS.Clock.Infrustructure.Compiler.Compiler.CompileFile ");
            }

        }


        private void BackupOldDLL(string dllPath)
        {
            if (File.Exists(dllPath))
            {
                FileInfo fi = new FileInfo(dllPath);
                DateTime d = DateTime.Now;
                string dateName = String.Format("{0}{1}{2}{3}{4}{5}", d.Year, d.Month, d.Day, d.Hour, d.Month, d.Second);
                if (!Directory.Exists(backupDir))
                {
                    Directory.CreateDirectory(backupDir);
                }
                DirectoryInfo di = Directory.CreateDirectory(backupDir + "\\" + dateName);
                File.Copy(dllPath, String.Format("{0}\\{1}", di.FullName, fi.Name),true);

            }

        }

        #endregion
    }
}