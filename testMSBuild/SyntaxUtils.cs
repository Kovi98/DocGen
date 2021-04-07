using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace testMSBuild
{
    public static class SyntaxUtils
    {
        public static LogWriter LogWriter { get; private set; }

        public static int ErrorCount { get; private set; }
        public async static Task<bool> MakeDocumentation(string solutionPath)
        {
            if (!MSBuildLocator.IsRegistered)
                MSBuildLocator.RegisterDefaults();

            var msWorkspace = MSBuildWorkspace.Create();

            var solution = await msWorkspace.OpenSolutionAsync(solutionPath);

            LogWriter = new LogWriter(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            LogWriter.Log("Solution: " + solution.FilePath);

            if (msWorkspace.Diagnostics.Count > 0)
            {
                foreach (var diag in msWorkspace.Diagnostics)
                {
                    Console.WriteLine(diag.Message);
                    LogWriter.Log(diag.Message, LogWriter.LogType.Warning);
                }
            }
            else
            {
                Console.WriteLine("Solution úspěšně načtena");
                LogWriter.Log("Solution úspěšně načtena");
            }

            //Procházení všech solution->projektů->souborů a procházení 1. vrstvy
            Console.WriteLine("Procházení 1. vrstvy");
            LogWriter.Log("Procházení 1. vrstvy");
            foreach (var projId in solution.ProjectIds)
            {
                var project = solution.GetProject(projId);

                //Omezení pouze na Csharp projekty
                if (project.Language != "C#")
                {
                    Console.WriteLine("Jazyk projektu " + project.Name + "nebyl definován jako C#, nalezeno: " + project.Language + ". Tento projekt bude přeskočen");
                    LogWriter.Log("Jazyk projektu " + project.Name + "nebyl definován jako C#, nalezeno: " + project.Language + ". Tento projekt bude přeskočen", LogWriter.LogType.Warning);
                    continue;
                }

                //Procházení souborů
                foreach (var docId in project.DocumentIds)
                {
                    var document = project.GetDocument(docId);

                    var documentEditor = await DocumentEditor.CreateAsync(document);

                    var syntaxTree = await document.GetSyntaxTreeAsync();

                    SearchAndMake(syntaxTree.GetRoot().DescendantNodes().Where(x => x.Parent?.Kind() == SyntaxKind.NamespaceDeclaration), documentEditor, 1);
                    
                    var newDocument = documentEditor.GetChangedDocument();
                    document = newDocument;
                    syntaxTree = await newDocument.GetSyntaxTreeAsync();
                    solution = solution.WithDocumentSyntaxRoot(newDocument.Id, syntaxTree.GetRoot());
                }
            }
            Console.WriteLine("Procházení 1. vrstvy ukončeno");
            LogWriter.Log("Procházení 1. vrstvy ukončeno");

            //Procházení všech solution->projektů->souborů a procházení 2. vrstvy
            Console.WriteLine("Procházení 2. vrstvy");
            LogWriter.Log("Procházení 2. vrstvy");
            foreach (var projId in solution.ProjectIds)
            {
                var project = solution.GetProject(projId);

                //Omezení pouze na Csharp projekty
                if (project.Language != "C#")
                {
                    Console.WriteLine("Jazyk projektu " + project.Name + "nebyl definován jako C#, nalezeno: " + project.Language + ". Tento projekt bude přeskočen");
                    LogWriter.Log("Jazyk projektu " + project.Name + "nebyl definován jako C#, nalezeno: " + project.Language + ". Tento projekt bude přeskočen", LogWriter.LogType.Warning);
                    continue;
                }

                //Procházení souborů
                foreach (var docId in project.DocumentIds)
                {
                    var document = project.GetDocument(docId);

                    var documentEditor = await DocumentEditor.CreateAsync(document);

                    var syntaxTree = await document.GetSyntaxTreeAsync();

                    SearchAndMake(syntaxTree.GetRoot().DescendantNodes().Where(x => x.Parent?.Parent?.Kind() == SyntaxKind.NamespaceDeclaration), documentEditor, 2);

                    var newDocument = documentEditor.GetChangedDocument();
                    document = newDocument;
                    syntaxTree = await newDocument.GetSyntaxTreeAsync();
                    solution = solution.WithDocumentSyntaxRoot(newDocument.Id, syntaxTree.GetRoot());
                }
            }
            Console.WriteLine("Procházení 2. vrstvy ukončeno");
            LogWriter.Log("Procházení 2. vrstvy ukončeno");

            //Procházení všech solution->projektů->souborů a procházení 3. vrstvy
            Console.WriteLine("Procházení 3. vrstvy");
            LogWriter.Log("Procházení 3. vrstvy");
            foreach (var projId in solution.ProjectIds)
            {
                var project = solution.GetProject(projId);

                //Omezení pouze na Csharp projekty
                if (project.Language != "C#")
                {
                    Console.WriteLine("Jazyk projektu " + project.Name + "nebyl definován jako C#, nalezeno: " + project.Language + ". Tento projekt bude přeskočen");
                    LogWriter.Log("Jazyk projektu " + project.Name + "nebyl definován jako C#, nalezeno: " + project.Language + ". Tento projekt bude přeskočen", LogWriter.LogType.Warning);
                    continue;
                }

                //Procházení souborů
                foreach (var docId in project.DocumentIds)
                {
                    var document = project.GetDocument(docId);

                    var documentEditor = await DocumentEditor.CreateAsync(document);

                    var syntaxTree = await document.GetSyntaxTreeAsync();

                    SearchAndMake(syntaxTree.GetRoot().DescendantNodes().Where(x => x.Parent?.Parent?.Parent?.Kind() == SyntaxKind.NamespaceDeclaration), documentEditor, 3);

                    var newDocument = documentEditor.GetChangedDocument();
                    document = newDocument;
                    syntaxTree = await newDocument.GetSyntaxTreeAsync();
                    solution = solution.WithDocumentSyntaxRoot(newDocument.Id, syntaxTree.GetRoot());
                }
            }
            Console.WriteLine("Procházení 3. vrstvy ukončeno");
            LogWriter.Log("Procházení 3. vrstvy ukončeno");

            //Procházení všech solution->projektů->souborů a procházení 4. vrstvy
            Console.WriteLine("Procházení 4. vrstvy");
            LogWriter.Log("Procházení 4. vrstvy");
            foreach (var projId in solution.ProjectIds)
            {
                var project = solution.GetProject(projId);

                //Omezení pouze na Csharp projekty
                if (project.Language != "C#")
                {
                    Console.WriteLine("Jazyk projektu " + project.Name + "nebyl definován jako C#, nalezeno: " + project.Language + ". Tento projekt bude přeskočen");
                    LogWriter.Log("Jazyk projektu " + project.Name + "nebyl definován jako C#, nalezeno: " + project.Language + ". Tento projekt bude přeskočen", LogWriter.LogType.Warning);
                    continue;
                }

                //Procházení souborů
                foreach (var docId in project.DocumentIds)
                {
                    var document = project.GetDocument(docId);

                    var documentEditor = await DocumentEditor.CreateAsync(document);

                    var syntaxTree = await document.GetSyntaxTreeAsync();

                    SearchAndMake(syntaxTree.GetRoot().DescendantNodes().Where(x => x.Parent?.Parent?.Parent?.Parent?.Kind() == SyntaxKind.NamespaceDeclaration), documentEditor, 4);

                    var newDocument = documentEditor.GetChangedDocument();
                    document = newDocument;
                    syntaxTree = await newDocument.GetSyntaxTreeAsync();
                    solution = solution.WithDocumentSyntaxRoot(newDocument.Id, syntaxTree.GetRoot());
                }
            }
            Console.WriteLine("Procházení 4. vrstvy ukončeno");
            LogWriter.Log("Procházení 4. vrstvy ukončeno");

            //Procházení všech solution->projektů->souborů a procházení 5. vrstvy
            Console.WriteLine("Procházení 5. vrstvy");
            LogWriter.Log("Procházení 5. vrstvy");
            foreach (var projId in solution.ProjectIds)
            {
                var project = solution.GetProject(projId);

                //Omezení pouze na Csharp projekty
                if (project.Language != "C#")
                {
                    Console.WriteLine("Jazyk projektu " + project.Name + "nebyl definován jako C#, nalezeno: " + project.Language + ". Tento projekt bude přeskočen");
                    LogWriter.Log("Jazyk projektu " + project.Name + "nebyl definován jako C#, nalezeno: " + project.Language + ". Tento projekt bude přeskočen", LogWriter.LogType.Warning);
                    continue;
                }

                //Procházení souborů
                foreach (var docId in project.DocumentIds)
                {
                    var document = project.GetDocument(docId);

                    var documentEditor = await DocumentEditor.CreateAsync(document);

                    var syntaxTree = await document.GetSyntaxTreeAsync();

                    SearchAndMake(syntaxTree.GetRoot().DescendantNodes().Where(x => x.Parent?.Parent?.Parent?.Parent?.Parent?.Kind() == SyntaxKind.NamespaceDeclaration), documentEditor, 5);

                    var newDocument = documentEditor.GetChangedDocument();
                    document = newDocument;
                    syntaxTree = await newDocument.GetSyntaxTreeAsync();
                    solution = solution.WithDocumentSyntaxRoot(newDocument.Id, syntaxTree.GetRoot());
                }
            }
            Console.WriteLine("Procházení 5. vrstvy ukončeno");
            LogWriter.Log("Procházení 5. vrstvy ukončeno");

            Console.WriteLine("Počet chyb při generování dokumentace: " + SyntaxUtils.ErrorCount);
            LogWriter.Log("Počet chyb při generování dokumentace: " + SyntaxUtils.ErrorCount);

            if (msWorkspace.TryApplyChanges(solution))
            {
                Console.WriteLine("Změněná solution byla úspěšně uložena");
                LogWriter.Log("Změněná solution byla úspěšně uložena");
            }
            else
            {
                Console.WriteLine("Změněná solution se nepodařila uložit");
                LogWriter.Log("Změněná solution se nepodařila uložit", LogWriter.LogType.Error);
            }

            return true;
        }

        /// <summary>
        /// Pomocná metoda pro procházení po vrstvách - nutno vrstvu odfiltrovat u inputu pomocí podmínky Parent...Kind() == SyntaxKind.NamespaceDeclaration
        /// </summary>
        /// <param name="nodes">Odfiltrovaný seznam objektů, který se má prohledávat</param>
        /// <param name="documentEditor">Document editor pro zaznamenávání změň</param>
        /// <param name="depth">Kolikátá je to vrstva - kvůli odřádkování</param>
        public static void SearchAndMake(IEnumerable<SyntaxNode> nodes, DocumentEditor documentEditor, int depth = 1)
        {
            //Procházení tříd
            foreach (var classNode in nodes.OfType<ClassDeclarationSyntax>())
            {
                var classComment = classNode.GetLeadingTrivia(); //Vrací dokumentaci k objektu - class
                if (classComment.Where(c => c.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) || c.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia)).Count() > 0)
                {
                    WriteColoredLine("Class " + classNode.Identifier.Text + " docs OK", ConsoleColor.Green);
                    LogWriter.Log("Class " + classNode.Identifier.Text + " docs OK");
                }
                else
                {
                    WriteColoredLine("Class " + classNode.Identifier.Text + " docs MISSING", ConsoleColor.Red);
                    LogWriter.Log("Class " + classNode.Identifier.Text + " docs MISSING");

                    try
                    {
                        documentEditor.ReplaceNode(classNode, SyntaxUtils.AddDocumentation(classNode, depth));

                        WriteColoredLine("Class " + classNode.Identifier.Text + " docs GENERATED", ConsoleColor.Green);
                        LogWriter.Log("Class " + classNode.Identifier.Text + " docs GENERATED");
                    }
                    catch (Exception e)
                    {
                        WriteColoredLine("Class " + classNode.Identifier.Text + " docs ERROR: " + e.Message, ConsoleColor.Red);
                        LogWriter.Log("Class " + classNode.Identifier.Text + " docs ERROR: " + e.Message, LogWriter.LogType.Error);
                        ErrorCount++;
                    }
                }
            }
            //Procházení interfaců
            foreach (var interfaceNode in nodes.OfType<InterfaceDeclarationSyntax>())
            {
                var classComment = interfaceNode.GetLeadingTrivia(); //Vrací dokumentaci k objektu - class
                if (classComment.Where(c => c.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) || c.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia)).Count() > 0)
                {
                    WriteColoredLine("Interface " + interfaceNode.Identifier.Text + " docs OK", ConsoleColor.Green);
                    LogWriter.Log("Interface " + interfaceNode.Identifier.Text + " docs OK");
                }
                else
                {
                    WriteColoredLine("Interface " + interfaceNode.Identifier.Text + " docs MISSING", ConsoleColor.Red);
                    LogWriter.Log("Interface " + interfaceNode.Identifier.Text + " docs MISSING");

                    try
                    {
                        documentEditor.ReplaceNode(interfaceNode, SyntaxUtils.AddDocumentation(interfaceNode, depth));

                        WriteColoredLine("Interface " + interfaceNode.Identifier.Text + " docs GENERATED", ConsoleColor.Green);
                        LogWriter.Log("Interface " + interfaceNode.Identifier.Text + " docs GENERATED");
                    }
                    catch (Exception e)
                    {
                        WriteColoredLine("Interface " + interfaceNode.Identifier.Text + " docs ERROR: " + e.Message, ConsoleColor.Red);
                        LogWriter.Log("Interface " + interfaceNode.Identifier.Text + " docs ERROR: " + e.Message, LogWriter.LogType.Error);
                        ErrorCount++;
                    }
                }
            }
            //Procházení enumů
            foreach (var enumNode in nodes.OfType<EnumDeclarationSyntax>())
            {
                var enumComment = enumNode.GetLeadingTrivia(); //Vrací dokumentaci k objektu - enum
                if (enumComment.Where(c => c.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) || c.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia)).Count() > 0)
                {
                    WriteColoredLine("Enum " + enumNode.Identifier.Text + " docs OK", ConsoleColor.Green);
                    LogWriter.Log("Enum " + enumNode.Identifier.Text + " docs OK");
                }
                else
                {
                    WriteColoredLine("Enum " + enumNode.Identifier.Text + " docs MISSING", ConsoleColor.Red);
                    LogWriter.Log("Enum " + enumNode.Identifier.Text + " docs MISSING");

                    try 
                    {
                        documentEditor.ReplaceNode(enumNode, SyntaxUtils.AddDocumentation(enumNode, depth));

                        WriteColoredLine("Enum " + enumNode.Identifier.Text + " docs GENERATED", ConsoleColor.Green);
                        LogWriter.Log("Enum " + enumNode.Identifier.Text + " docs GENERATED");
                    }
                    catch (Exception e)
                    {
                        WriteColoredLine("Enum " + enumNode.Identifier.Text + " docs ERROR: " + e.Message, ConsoleColor.Red);
                        LogWriter.Log("Enum " + enumNode.Identifier.Text + " docs ERROR: " + e.Message, LogWriter.LogType.Error);
                        ErrorCount++;
                    }
                }
            }
            //Procházení structur
            foreach (var structNode in nodes.OfType<StructDeclarationSyntax>())
            {
                var structComment = structNode.GetLeadingTrivia(); //Vrací dokumentaci k objektu - struct
                if (structComment.Where(c => c.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) || c.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia)).Count() > 0)
                {
                    WriteColoredLine("Struct " + structNode.Identifier.Text + " docs OK", ConsoleColor.Green);
                    LogWriter.Log("Struct " + structNode.Identifier.Text + " docs OK");
                }
                else
                {
                    WriteColoredLine("Struct " + structNode.Identifier.Text + " docs MISSING", ConsoleColor.Red);
                    LogWriter.Log("Struct " + structNode.Identifier.Text + " docs MISSING");

                    try
                    {
                        documentEditor.ReplaceNode(structNode, SyntaxUtils.AddDocumentation(structNode, depth));

                        WriteColoredLine("Struct " + structNode.Identifier.Text + " docs GENERATED", ConsoleColor.Green);
                        LogWriter.Log("Struct " + structNode.Identifier.Text + " docs GENERATED");
                    }
                    catch (Exception e)
                    {
                        WriteColoredLine("Struct " + structNode.Identifier.Text + " docs ERROR: " + e.Message, ConsoleColor.Red);
                        LogWriter.Log("Struct " + structNode.Identifier.Text + " docs ERROR: " + e.Message, LogWriter.LogType.Error);
                        ErrorCount++;
                    }
                }
            }
            //Procházení metod
            foreach (var methodNode in nodes.OfType<MethodDeclarationSyntax>())
            {
                var methodComment = methodNode.GetLeadingTrivia(); //Vrací dokumentaci k objektu - method
                if (methodComment.Where(c => c.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) || c.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia)).Count() > 0)
                {
                    WriteColoredLine("Method " + methodNode.Identifier.Text + " docs OK", ConsoleColor.Green);
                    LogWriter.Log("Method " + methodNode.Identifier.Text + " docs OK");
                }
                else
                {
                    WriteColoredLine("Method " + methodNode.Identifier.Text + " docs MISSING", ConsoleColor.Red);
                    LogWriter.Log("Method " + methodNode.Identifier.Text + " docs MISSING");

                    try
                    {
                        documentEditor.ReplaceNode(methodNode, SyntaxUtils.AddDocumentation(methodNode, depth));

                        WriteColoredLine("Method " + methodNode.Identifier.Text + " docs GENERATED", ConsoleColor.Green);
                        LogWriter.Log("Method " + methodNode.Identifier.Text + " docs GENERATED");
                    }
                    catch (Exception e)
                    {
                        WriteColoredLine("Method " + methodNode.Identifier.Text + " docs ERROR: " + e.Message, ConsoleColor.Red);
                        LogWriter.Log("Method " + methodNode.Identifier.Text + " docs ERROR: " + e.Message, LogWriter.LogType.Error);
                        ErrorCount++;
                    }
                }
            }
            //Procházení konstruktorů
            foreach (var constructorNode in nodes.OfType<ConstructorDeclarationSyntax>())
            {
                var constructorComment = constructorNode.GetLeadingTrivia(); //Vrací dokumentaci k objektu - constructor
                if (constructorComment.Where(c => c.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) || c.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia)).Count() > 0)
                {
                    WriteColoredLine("Constructor " + constructorNode.Identifier.Text + " docs OK", ConsoleColor.Green);
                    LogWriter.Log("Constructor " + constructorNode.Identifier.Text + " docs OK");
                }
                else
                {
                    WriteColoredLine("Constructor " + constructorNode.Identifier.Text + " docs MISSING", ConsoleColor.Red);
                    LogWriter.Log("Constructor " + constructorNode.Identifier.Text + " docs MISSING");

                    try
                    {
                        documentEditor.ReplaceNode(constructorNode, SyntaxUtils.AddDocumentation(constructorNode, depth));

                        WriteColoredLine("Constructor " + constructorNode.Identifier.Text + " docs GENERATED", ConsoleColor.Green);
                        LogWriter.Log("Constructor " + constructorNode.Identifier.Text + " docs GENERATED");
                    }
                    catch (Exception e)
                    {
                        WriteColoredLine("Constructor " + constructorNode.Identifier.Text + " docs ERROR: " + e.Message, ConsoleColor.Red);
                        LogWriter.Log("Constructor " + constructorNode.Identifier.Text + " docs ERROR: " + e.Message, LogWriter.LogType.Error);
                        ErrorCount++;
                    }
                }
            }
            //Procházení property
            foreach (var propertyNode in nodes.OfType<PropertyDeclarationSyntax>())
            {
                var propertyComment = propertyNode.GetLeadingTrivia(); //Vrací dokumentaci k objektu - property
                if (propertyComment.Where(c => c.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) || c.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia)).Count() > 0)
                {
                    WriteColoredLine("Property " + propertyNode.Identifier.Text + " docs OK", ConsoleColor.Green);
                    LogWriter.Log("Property " + propertyNode.Identifier.Text + " docs OK");
                }
                else
                {
                    WriteColoredLine("Property " + propertyNode.Identifier.Text + " docs MISSING", ConsoleColor.Red);
                    LogWriter.Log("Property " + propertyNode.Identifier.Text + " docs MISSING");

                    try
                    {
                        documentEditor.ReplaceNode(propertyNode, SyntaxUtils.AddDocumentation(propertyNode, depth));

                        WriteColoredLine("Property " + propertyNode.Identifier.Text + " docs GENERATED", ConsoleColor.Green);
                        LogWriter.Log("Property " + propertyNode.Identifier.Text + " docs GENERATED");
                    }
                    catch (Exception e)
                    {
                        WriteColoredLine("Property " + propertyNode.Identifier.Text + " docs ERROR: " + e.Message, ConsoleColor.Red);
                        LogWriter.Log("Property " + propertyNode.Identifier.Text + " docs ERROR: " + e.Message, LogWriter.LogType.Error);
                        ErrorCount++;
                    }
                }
            }
            //Procházení enum položek
            foreach (var enumMemberNode in nodes.OfType<EnumMemberDeclarationSyntax>())
            {
                var enumMemberComment = enumMemberNode.GetLeadingTrivia(); //Vrací dokumentaci k objektu - class
                if (enumMemberComment.Where(c => c.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) || c.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia)).Count() > 0)
                {
                    WriteColoredLine("Enum member " + enumMemberNode.Identifier.Text + " docs OK", ConsoleColor.Green);
                    LogWriter.Log("Enum member " + enumMemberNode.Identifier.Text + " docs OK");
                }
                else
                {
                    WriteColoredLine("Enum member " + enumMemberNode.Identifier.Text + " docs MISSING", ConsoleColor.Red);
                    LogWriter.Log("Enum member " + enumMemberNode.Identifier.Text + " docs MISSING");

                    try
                    {
                        documentEditor.ReplaceNode(enumMemberNode, SyntaxUtils.AddDocumentation(enumMemberNode, depth));

                        WriteColoredLine("Enum member " + enumMemberNode.Identifier.Text + " docs GENERATED", ConsoleColor.Green);
                        LogWriter.Log("Enum member " + enumMemberNode.Identifier.Text + " docs GENERATED");
                    }
                    catch (Exception e)
                    {
                        WriteColoredLine("Enum member " + enumMemberNode.Identifier.Text + " docs ERROR: " + e.Message, ConsoleColor.Red);
                        LogWriter.Log("Enum member " + enumMemberNode.Identifier.Text + " docs ERROR: " + e.Message, LogWriter.LogType.Error);
                        ErrorCount++;
                    }
                }
            }
            //Procházení proměnných
            foreach (var fieldNode in nodes.OfType<FieldDeclarationSyntax>())
            {
                if (!(fieldNode.Modifiers.Any(x => x.IsKind(SyntaxKind.ConstKeyword) || x.IsKind(SyntaxKind.PublicKeyword) || x.IsKind(SyntaxKind.ProtectedKeyword))))
                    continue;

                var fieldComment = fieldNode.GetLeadingTrivia(); //Vrací dokumentaci k objektu - class
                if (fieldNode.Declaration.Variables.Count > 1)
                {
                    WriteColoredLine("Field " + String.Join(", ", fieldNode.Declaration.Variables.Select(x => x.Identifier.Text)) + " docs ERROR: Deklarace na 1 řádku - chybný zápis", ConsoleColor.Red);
                    LogWriter.Log("Field " + String.Join(", ", fieldNode.Declaration.Variables.Select(x => x.Identifier.Text)) + " docs ERROR: Deklarace na 1 řádku - chybný zápis");
                    continue;
                }
                if (fieldComment.Where(c => c.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) || c.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia)).Count() > 0)
                {
                    WriteColoredLine("Field " + fieldNode.Declaration.Variables.FirstOrDefault().Identifier.Text + " docs OK", ConsoleColor.Green);
                    LogWriter.Log("Field " + fieldNode.Declaration.Variables.FirstOrDefault().Identifier.Text + " docs OK");
                }
                else
                {
                    WriteColoredLine("Field " + fieldNode.Declaration.Variables.FirstOrDefault().Identifier.Text + " docs MISSING", ConsoleColor.Red);
                    LogWriter.Log("Field " + fieldNode.Declaration.Variables.FirstOrDefault().Identifier.Text + " docs MISSING");

                    try
                    {
                        documentEditor.ReplaceNode(fieldNode, SyntaxUtils.AddDocumentation(fieldNode, depth));

                        WriteColoredLine("Field " + fieldNode.Declaration.Variables.FirstOrDefault().Identifier.Text + " docs GENERATED", ConsoleColor.Green);
                        LogWriter.Log("Field " + fieldNode.Declaration.Variables.FirstOrDefault().Identifier.Text + " docs GENERATED");
                    }
                    catch (Exception e)
                    {
                        WriteColoredLine("Field " + fieldNode.Declaration.Variables.FirstOrDefault().Identifier.Text + " docs ERROR: " + e.Message, ConsoleColor.Red);
                        LogWriter.Log("Field " + fieldNode.Declaration.Variables.FirstOrDefault().Identifier.Text + " docs ERROR: " + e.Message, LogWriter.LogType.Error);
                        ErrorCount++;
                    }
                }
            }
            //Procházení delegátů
            foreach (var delegateNode in nodes.OfType<DelegateDeclarationSyntax>())
            {
                var delegateComment = delegateNode.GetLeadingTrivia(); //Vrací dokumentaci k objektu - class
                if (delegateComment.Where(c => c.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) || c.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia)).Count() > 0)
                {
                    WriteColoredLine("Delegate " + delegateNode.Identifier.Text + " docs OK", ConsoleColor.Green);
                    LogWriter.Log("Delegate " + delegateNode.Identifier.Text + " docs OK");
                }
                else
                {
                    WriteColoredLine("Delegate " + delegateNode.Identifier.Text + " docs MISSING", ConsoleColor.Red);
                    LogWriter.Log("Delegate " + delegateNode.Identifier.Text + " docs MISSING");

                    try
                    {
                        documentEditor.ReplaceNode(delegateNode, SyntaxUtils.AddDocumentation(delegateNode, depth));

                        WriteColoredLine("Delegate " + delegateNode.Identifier.Text + " docs GENERATED", ConsoleColor.Green);
                        LogWriter.Log("Delegate " + delegateNode.Identifier.Text + " docs GENERATED");
                    }
                    catch (Exception e)
                    {
                        WriteColoredLine("Delegate " + delegateNode.Identifier.Text + " docs ERROR: " + e.Message, ConsoleColor.Red);
                        LogWriter.Log("Delegate " + delegateNode.Identifier.Text + " docs ERROR: " + e.Message, LogWriter.LogType.Error);
                        ErrorCount++;
                    }
                }
            }
            //Procházení eventů
            foreach (var eventFieldNode in nodes.OfType<EventFieldDeclarationSyntax>())
            {
                var eventFieldComment = eventFieldNode.GetLeadingTrivia(); //Vrací dokumentaci k objektu - class
                if (eventFieldNode.Declaration.Variables.Count > 1)
                {
                    WriteColoredLine("Event Field " + String.Join(", ", eventFieldNode.Declaration.Variables.Select(x => x.Identifier.Text)) + " docs ERROR: Deklarace na 1 řádku - chybný zápis", ConsoleColor.Red);
                    LogWriter.Log("Event Field " + String.Join(", ", eventFieldNode.Declaration.Variables.Select(x => x.Identifier.Text)) + " docs ERROR: Deklarace na 1 řádku - chybný zápis");
                    continue;
                }
                if (eventFieldComment.Where(c => c.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) || c.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia)).Count() > 0)
                {
                    WriteColoredLine("Event Field " + eventFieldNode.Declaration.Variables.FirstOrDefault().Identifier.Text + " docs OK", ConsoleColor.Green);
                    LogWriter.Log("Event Field " + eventFieldNode.Declaration.Variables.FirstOrDefault().Identifier.Text + " docs OK");
                }
                else
                {
                    WriteColoredLine("Event Field " + eventFieldNode.Declaration.Variables.FirstOrDefault().Identifier.Text + " docs MISSING", ConsoleColor.Red);
                    LogWriter.Log("Event Field " + eventFieldNode.Declaration.Variables.FirstOrDefault().Identifier.Text + " docs MISSING");

                    try
                    {
                        documentEditor.ReplaceNode(eventFieldNode, SyntaxUtils.AddDocumentation(eventFieldNode, depth));

                        WriteColoredLine("Event Field " + eventFieldNode.Declaration.Variables.FirstOrDefault().Identifier.Text + " docs GENERATED", ConsoleColor.Green);
                        LogWriter.Log("Event Field " + eventFieldNode.Declaration.Variables.FirstOrDefault().Identifier.Text + " docs GENERATED");
                    }
                    catch (Exception e)
                    {
                        WriteColoredLine("Event Field " + eventFieldNode.Declaration.Variables.FirstOrDefault().Identifier.Text + " docs ERROR: " + e.Message, ConsoleColor.Red);
                        LogWriter.Log("Event Field " + eventFieldNode.Declaration.Variables.FirstOrDefault().Identifier.Text + " docs ERROR: " + e.Message, LogWriter.LogType.Error);
                        ErrorCount++;
                    }
                }
            }
            //Procházení indexerů
            foreach (var indexerNode in nodes.OfType<IndexerDeclarationSyntax>())
            {
                var indexerComment = indexerNode.GetLeadingTrivia(); //Vrací dokumentaci k objektu - method
                string name = ((IdentifierNameSyntax)indexerNode.Type).Identifier.ValueText;
                if (indexerComment.Where(c => c.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) || c.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia)).Count() > 0)
                {
                    WriteColoredLine("Indexer " + name + ".Indexer docs OK", ConsoleColor.Green);
                    LogWriter.Log("Indexer " + name + ".Indexer docs OK");
                }
                else
                {
                    WriteColoredLine("Indexer " + name + ".Indexer docs MISSING", ConsoleColor.Red);
                    LogWriter.Log("Indexer " + name + ".Indexer docs MISSING");

                    try
                    {
                        documentEditor.ReplaceNode(indexerNode, SyntaxUtils.AddDocumentation(indexerNode, depth));

                        WriteColoredLine("Indexer " + name + ".Indexer docs GENERATED", ConsoleColor.Green);
                        LogWriter.Log("Indexer " + name + ".Indexer docs GENERATED");
                    }
                    catch (Exception e)
                    {
                        WriteColoredLine("Indexer " + name + ".Indexer docs ERROR: " + e.Message, ConsoleColor.Red);
                        LogWriter.Log("Indexer " + name + ".Indexer docs ERROR: " + e.Message, LogWriter.LogType.Error);
                        ErrorCount++;
                    }
                }
            }
            //Procházení operátorů
            foreach (var operatorNode in nodes.OfType<OperatorDeclarationSyntax>())
            {
                var operatorComment = operatorNode.GetLeadingTrivia(); //Vrací dokumentaci k objektu - method
                string name = string.Empty;
                switch (operatorNode.Parent?.Kind())
                {
                    case SyntaxKind.ClassDeclaration:
                        name = ((ClassDeclarationSyntax)operatorNode.Parent).Identifier.ValueText;
                        break;
                    case SyntaxKind.InterfaceDeclaration:
                        name = ((InterfaceDeclarationSyntax)operatorNode.Parent).Identifier.ValueText;
                        break;
                    case SyntaxKind.StructDeclaration:
                        name = ((StructDeclarationSyntax)operatorNode.Parent).Identifier.ValueText;
                        break;
                }
                if (operatorComment.Where(c => c.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) || c.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia)).Count() > 0)
                {
                    WriteColoredLine("Operator " + name + "." + operatorNode.OperatorToken.ValueText + " docs OK", ConsoleColor.Green);
                    LogWriter.Log("Operator " + name + "." + operatorNode.OperatorToken.ValueText + " docs OK");
                }
                else
                {
                    WriteColoredLine("Operator " + name + "." + operatorNode.OperatorToken.ValueText + " docs MISSING", ConsoleColor.Red);
                    LogWriter.Log("Operator " + name + "." + operatorNode.OperatorToken.ValueText + " docs MISSING");

                    try
                    {
                        documentEditor.ReplaceNode(operatorNode, SyntaxUtils.AddDocumentation(operatorNode, depth));

                        WriteColoredLine("Operator " + name + "." + operatorNode.OperatorToken.ValueText + " docs GENERATED", ConsoleColor.Green);
                        LogWriter.Log("Operator " + name + "." + operatorNode.OperatorToken.ValueText + " docs GENERATED");
                    }
                    catch (Exception e)
                    {
                        WriteColoredLine("Operator " + name + "." + operatorNode.OperatorToken.ValueText + " docs ERROR: " + e.Message, ConsoleColor.Red);
                        LogWriter.Log("Operator " + name + "." + operatorNode.OperatorToken.ValueText + " docs ERROR: " + e.Message, LogWriter.LogType.Error);
                        ErrorCount++;
                    }
                }
            }
            //Procházení conversion operátorů
            foreach (var operatorNode in nodes.OfType<ConversionOperatorDeclarationSyntax>())
            {
                var operatorComment = operatorNode.GetLeadingTrivia(); //Vrací dokumentaci k objektu - method
                string name = string.Empty;
                switch (operatorNode.Parent?.Kind())
                {
                    case SyntaxKind.ClassDeclaration:
                        name = ((ClassDeclarationSyntax)operatorNode.Parent).Identifier.ValueText;
                        break;
                    case SyntaxKind.InterfaceDeclaration:
                        name = ((InterfaceDeclarationSyntax)operatorNode.Parent).Identifier.ValueText;
                        break;
                    case SyntaxKind.StructDeclaration:
                        name = ((StructDeclarationSyntax)operatorNode.Parent).Identifier.ValueText;
                        break;
                }
                if (operatorComment.Where(c => c.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) || c.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia)).Count() > 0)
                {
                    WriteColoredLine("Operator " + name + "." + operatorNode.OperatorKeyword.ValueText + " docs OK", ConsoleColor.Green);
                    LogWriter.Log("Operator " + name + "." + operatorNode.OperatorKeyword.ValueText + " docs OK");
                }
                else
                {
                    WriteColoredLine("Operator " + name + "." + operatorNode.OperatorKeyword.ValueText + " docs MISSING", ConsoleColor.Red);
                    LogWriter.Log("Operator " + name + "." + operatorNode.OperatorKeyword.ValueText + " docs MISSING");

                    try
                    {
                        documentEditor.ReplaceNode(operatorNode, SyntaxUtils.AddDocumentation(operatorNode, depth));

                        WriteColoredLine("Operator " + name + "." + operatorNode.OperatorKeyword.ValueText + " docs GENERATED", ConsoleColor.Green);
                        LogWriter.Log("Operator " + name + "." + operatorNode.OperatorKeyword.ValueText + " docs GENERATED");
                    }
                    catch (Exception e)
                    {
                        WriteColoredLine("Operator " + name + "." + operatorNode.OperatorKeyword.ValueText + " docs ERROR: " + e.Message, ConsoleColor.Red);
                        LogWriter.Log("Operator " + name + "." + operatorNode.OperatorKeyword.ValueText + " docs ERROR: " + e.Message, LogWriter.LogType.Error);
                        ErrorCount++;
                    }
                }
            }
        }
        public static void WriteColoredLine(string text, ConsoleColor color)
        {
            Console.ResetColor();
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static ClassDeclarationSyntax AddDocumentation(ClassDeclarationSyntax classNode, int depth = 1)
        {
            var triviaList = classNode.GetLeadingTrivia();
            var documentation = new List<SyntaxTrivia>();
            documentation.AddRange(CreateDoc(DocumentationType.Summary, "Implementuje funkčnost pro " + classNode.Identifier.ValueText + ".", depth));
            ClassDeclarationSyntax newNode = classNode.InsertTriviaBefore(triviaList.Last(), documentation);
            return newNode;
        }
        public static InterfaceDeclarationSyntax AddDocumentation(InterfaceDeclarationSyntax interfaceNode, int depth = 1)
        {
            var triviaList = interfaceNode.GetLeadingTrivia();
            var documentation = new List<SyntaxTrivia>();
            documentation.AddRange(CreateDoc(DocumentationType.Summary, "Rozhraní pro implementaci " + interfaceNode.Identifier.ValueText + ".", depth));
            InterfaceDeclarationSyntax newNode = interfaceNode.InsertTriviaBefore(triviaList.Last(), documentation);
            return newNode;
        }
        public static StructDeclarationSyntax AddDocumentation(StructDeclarationSyntax structNode, int depth = 1)
        {
            var triviaList = structNode.GetLeadingTrivia();
            var documentation = new List<SyntaxTrivia>();
            documentation.AddRange(CreateDoc(DocumentationType.Summary, "Úložiště dat pro " + structNode.Identifier.ValueText + ".", depth));
            StructDeclarationSyntax newNode = structNode.InsertTriviaBefore(triviaList.Last(), documentation);
            return newNode;
        }
        public static EnumDeclarationSyntax AddDocumentation(EnumDeclarationSyntax enumNode, int depth = 1)
        {
            var triviaList = enumNode.GetLeadingTrivia();
            var documentation = new List<SyntaxTrivia>();
            documentation.AddRange(CreateDoc(DocumentationType.Summary, "Výčet položek " + enumNode.Identifier.ValueText + ".", depth));
            EnumDeclarationSyntax newNode = enumNode.InsertTriviaBefore(triviaList.Last(), documentation);
            return newNode;
        }
        public static MethodDeclarationSyntax AddDocumentation(MethodDeclarationSyntax methodNode, int depth = 1)
        {
            var triviaList = methodNode.GetLeadingTrivia();
            var documentation = new List<SyntaxTrivia>();
            documentation.AddRange(CreateDoc(DocumentationType.Summary, "Implementace funkce " + methodNode.Identifier.ValueText + ".", depth)); //summary
            if (methodNode.ParameterList.Parameters.Count > 0)
                documentation.AddRange(CreateDoc(DocumentationType.Parameter, "PARAMS", depth, methodNode.ParameterList.Parameters.Select(x => x.Identifier.ValueText).ToArray()));
            if (!(methodNode.ReturnType is null) && !methodNode.ReturnType.ToString().Contains("void"))
                documentation.AddRange(CreateDoc(DocumentationType.Result, "Vrací hodnotu " + methodNode.ReturnType.ToString() + ".", depth)); //return
            MethodDeclarationSyntax newNode = methodNode.InsertTriviaBefore(triviaList.Last(), documentation);
            return newNode;
        }
        public static ConstructorDeclarationSyntax AddDocumentation(ConstructorDeclarationSyntax constructorNode, int depth = 1)
        {
            var triviaList = constructorNode.GetLeadingTrivia();
            var documentation = new List<SyntaxTrivia>();
            documentation.AddRange(CreateDoc(DocumentationType.Summary, "Implementace funkce " + constructorNode.Identifier.ValueText + ".", depth));

            ConstructorDeclarationSyntax newNode = constructorNode.InsertTriviaBefore(triviaList.Last(), documentation);
            return newNode;
        }
        public static PropertyDeclarationSyntax AddDocumentation(PropertyDeclarationSyntax propertyNode, int depth = 1)
        {
            var triviaList = propertyNode.GetLeadingTrivia();
            var documentation = new List<SyntaxTrivia>();
            documentation.AddRange(CreateDoc(DocumentationType.Summary, "Implementace funkce " + propertyNode.Identifier.ValueText + ".", depth));

            PropertyDeclarationSyntax newNode = propertyNode.InsertTriviaBefore(triviaList.Last(), documentation);
            return newNode;
        }
        public static EnumMemberDeclarationSyntax AddDocumentation(EnumMemberDeclarationSyntax enumMemberNode, int depth = 1)
        {
            var triviaList = enumMemberNode.GetLeadingTrivia();
            var documentation = new List<SyntaxTrivia>();
            documentation.AddRange(CreateDoc(DocumentationType.Summary, "Hodnota reprezentující " + enumMemberNode.Identifier.ValueText + ".", depth));

            EnumMemberDeclarationSyntax newNode = enumMemberNode.InsertTriviaBefore(triviaList.Last(), documentation);
            return newNode;
        }
        public static FieldDeclarationSyntax AddDocumentation(FieldDeclarationSyntax fieldNode, int depth = 1)
        {
            var triviaList = fieldNode.GetLeadingTrivia();
            var documentation = new List<SyntaxTrivia>();
            string prefix = String.Empty;

            if (fieldNode.Modifiers.Any(x => x.IsKind(SyntaxKind.ConstKeyword)))
            {
                prefix = "Konstanta ";
            }
            else if (fieldNode.Modifiers.Any(x => x.IsKind(SyntaxKind.PublicKeyword) || x.IsKind(SyntaxKind.ProtectedKeyword)))
            {
                prefix = "Proměnná ";
            }
            else
            {
                return fieldNode;
            }
            documentation.AddRange(CreateDoc(DocumentationType.Summary, prefix + fieldNode.Declaration.Variables.FirstOrDefault().Identifier.ValueText + ".", depth));

            FieldDeclarationSyntax newNode = fieldNode.InsertTriviaBefore(triviaList.Last(), documentation);
            return newNode;
        }
        public static DelegateDeclarationSyntax AddDocumentation(DelegateDeclarationSyntax delegateNode, int depth = 1)
        {
            var triviaList = delegateNode.GetLeadingTrivia();
            var documentation = new List<SyntaxTrivia>();
            documentation.AddRange(CreateDoc(DocumentationType.Summary, "Delagát pro " + delegateNode.Identifier.ValueText + ".", depth));

            DelegateDeclarationSyntax newNode = delegateNode.InsertTriviaBefore(triviaList.Last(), documentation);
            return newNode;
        }
        public static EventFieldDeclarationSyntax AddDocumentation(EventFieldDeclarationSyntax eventFieldNode, int depth = 1)
        {
            var triviaList = eventFieldNode.GetLeadingTrivia();
            var documentation = new List<SyntaxTrivia>();
            documentation.AddRange(CreateDoc(DocumentationType.Summary, "Událost " + eventFieldNode.Declaration.Variables.FirstOrDefault().Identifier.ValueText + ".", depth));

            EventFieldDeclarationSyntax newNode = eventFieldNode.InsertTriviaBefore(triviaList.Last(), documentation);
            return newNode;
        }
        public static IndexerDeclarationSyntax AddDocumentation(IndexerDeclarationSyntax indexerNode, int depth = 1)
        {
            var triviaList = indexerNode.GetLeadingTrivia();
            var documentation = new List<SyntaxTrivia>();
            string summary = String.Empty;
            string parameter = String.Empty;
            string result = String.Empty;

            if (indexerNode.AccessorList.Accessors.Any(x => x.IsKind(SyntaxKind.SetAccessorDeclaration)))
            {
                if (indexerNode.ParameterList.Parameters.Any(x => x.ToString().Contains("string")))
                {
                    summary = "Získá nebo nastaví element pro dané jméno";
                    parameter = "Jméno elementu";
                    result = "Element pro dané jméno";
                }
                else
                {
                    summary = "Získá nebo nastaví element pro daný index";
                    parameter = "Index elementu";
                    result = "Element pro daný index";
                }
            }
            else
            {
                if (indexerNode.ParameterList.Parameters.Any(x => x.ToString().Contains("string")))
                {
                    summary = "Získá element pro dané jméno";
                    parameter = "Jméno elementu";
                    result = "Element pro dané jméno";
                }
                else
                {
                    summary = "Získá element pro daný index";
                    parameter = "Index elementu";
                    result = "Element pro daný index";
                }
            }

            documentation.AddRange(CreateDoc(DocumentationType.Summary, summary + ".", depth)); //summary
            if (indexerNode.ParameterList.Parameters.Count > 0)
                documentation.AddRange(CreateDoc(DocumentationType.Parameter, parameter + ".", depth, indexerNode.ParameterList.Parameters.Select(x => x.Identifier.ValueText).ToArray()));
            documentation.AddRange(CreateDoc(DocumentationType.Result, result + ".", depth)); //return
            IndexerDeclarationSyntax newNode = indexerNode.InsertTriviaBefore(triviaList.Last(), documentation);
            return newNode;
        }
        public static OperatorDeclarationSyntax AddDocumentation(OperatorDeclarationSyntax operatorNode, int depth = 1)
        {
            var triviaList = operatorNode.GetLeadingTrivia();
            var documentation = new List<SyntaxTrivia>();
            documentation.AddRange(CreateDoc(DocumentationType.Summary, "Implementace operátoru " + operatorNode.OperatorToken.ValueText + ".", depth)); //summary
            if (operatorNode.ParameterList.Parameters.Count > 0)
                documentation.AddRange(CreateDoc(DocumentationType.Parameter, "PARAMS", depth, operatorNode.ParameterList.Parameters.Select(x => x.Identifier.ValueText).ToArray()));
            if (!(operatorNode.ReturnType is null) && !operatorNode.ReturnType.ToString().Contains("void"))
                documentation.AddRange(CreateDoc(DocumentationType.Result, "Vrací hodnotu " + operatorNode.ReturnType.ToString() + ".", depth)); //return
            OperatorDeclarationSyntax newNode = operatorNode.InsertTriviaBefore(triviaList.Last(), documentation);
            return newNode;
        }
        public static ConversionOperatorDeclarationSyntax AddDocumentation(ConversionOperatorDeclarationSyntax operatorNode, int depth = 1)
        {
            var triviaList = operatorNode.GetLeadingTrivia();
            var documentation = new List<SyntaxTrivia>();
            string prefix = operatorNode.ImplicitOrExplicitKeyword.IsKind(SyntaxKind.ImplicitKeyword) ? "implicitního" : "explicitního";
            documentation.AddRange(CreateDoc(DocumentationType.Summary, "Implementace "+ prefix +" operátoru.", depth)); //summary
            if (operatorNode.ParameterList.Parameters.Count > 0)
                documentation.AddRange(CreateDoc(DocumentationType.Parameter, "PARAMS", depth, operatorNode.ParameterList.Parameters.Select(x => x.Identifier.ValueText).ToArray()));
            ConversionOperatorDeclarationSyntax newNode = operatorNode.InsertTriviaBefore(triviaList.Last(), documentation);
            return newNode;
        }
        public static List<SyntaxTrivia> CreateDoc(DocumentationType kind, string inside, int depth = 1, string[] paramArray = null)
        {
            //kontrola konzistence parametrů
            if (kind == DocumentationType.Parameter && (paramArray is null || paramArray.Length == 0) && (inside == "PARAM" || inside is null))
                throw new InvalidOperationException("Došlo k nekonzistenci parametrů pro kind=param");

            var result = new List<SyntaxTrivia>();
            string whiteSpace = string.Empty;

            for (int i=0; i < depth; i++)
            {
                whiteSpace += "    ";
            }

            //Nahrazení znamének < > HTML tagem
            if (!(inside is null) && (inside.Contains('<') || inside.Contains('>')))
                inside = inside.Replace("<", "&lt;").Replace(">", "&gt;");

            switch (kind)
            {
                case DocumentationType.Summary:
                    {
                        var code = SyntaxFactory.TriviaList(
            SyntaxFactory.Trivia(
                SyntaxFactory.DocumentationCommentTrivia(
                    SyntaxKind.SingleLineDocumentationCommentTrivia,
                    SyntaxFactory.List<XmlNodeSyntax>(
                        new XmlNodeSyntax[]{
                            SyntaxFactory.XmlText()
                            .WithTextTokens(
                                SyntaxFactory.TokenList(
                                    SyntaxFactory.XmlTextLiteral(
                                        SyntaxFactory.TriviaList(
                                            SyntaxFactory.DocumentationCommentExterior(whiteSpace + "///")),
                                        " ",
                                        " ",
                                        SyntaxFactory.TriviaList()))),
                            SyntaxFactory.XmlExampleElement(
                                SyntaxFactory.SingletonList<XmlNodeSyntax>(
                                    SyntaxFactory.XmlText()
                                    .WithTextTokens(
                                        SyntaxFactory.TokenList(
                                            new []{
                                                SyntaxFactory.XmlTextNewLine(
                                                    SyntaxFactory.TriviaList(),
                                                    Environment.NewLine,
                                                    Environment.NewLine,
                                                    SyntaxFactory.TriviaList()),
                                                SyntaxFactory.XmlTextLiteral(
                                                    SyntaxFactory.TriviaList(
                                                        SyntaxFactory.DocumentationCommentExterior(whiteSpace + "///")),
                                                    " " + inside,
                                                    " " + inside,
                                                    SyntaxFactory.TriviaList()),
                                                SyntaxFactory.XmlTextNewLine(
                                                    SyntaxFactory.TriviaList(),
                                                    Environment.NewLine,
                                                    Environment.NewLine,
                                                    SyntaxFactory.TriviaList()),
                                                SyntaxFactory.XmlTextLiteral(
                                                    SyntaxFactory.TriviaList(
                                                        SyntaxFactory.DocumentationCommentExterior(whiteSpace + "///")),
                                                    " ",
                                                    " ",
                                                    SyntaxFactory.TriviaList())}))))
                            .WithStartTag(
                                SyntaxFactory.XmlElementStartTag(
                                    SyntaxFactory.XmlName(
                                        SyntaxFactory.Identifier("summary"))))
                            .WithEndTag(
                                SyntaxFactory.XmlElementEndTag(
                                    SyntaxFactory.XmlName(
                                        SyntaxFactory.Identifier("summary")))),
                            SyntaxFactory.XmlText()
                            .WithTextTokens(
                                SyntaxFactory.TokenList(
                                    SyntaxFactory.XmlTextNewLine(
                                        SyntaxFactory.TriviaList(),
                                        Environment.NewLine,
                                        Environment.NewLine,
                                        SyntaxFactory.TriviaList())))}))));
                        result.AddRange(code);
                        break;
                    }
                case DocumentationType.Parameter:
                    {
                        if (inside is null || inside == "PARAMS")
                        {
                            foreach (var param in paramArray)
                            {
                                var code = SyntaxFactory.TriviaList(
                        SyntaxFactory.Trivia(
                            SyntaxFactory.DocumentationCommentTrivia(
                                SyntaxKind.SingleLineDocumentationCommentTrivia,
                                SyntaxFactory.List<XmlNodeSyntax>(
                                    new XmlNodeSyntax[]{
                            SyntaxFactory.XmlText()
                            .WithTextTokens(
                                SyntaxFactory.TokenList(
                                    SyntaxFactory.XmlTextLiteral(
                                        SyntaxFactory.TriviaList(
                                            SyntaxFactory.DocumentationCommentExterior(whiteSpace + "///")),
                                        " ",
                                        " ",
                                        SyntaxFactory.TriviaList()))),
                            SyntaxFactory.XmlExampleElement(
                                SyntaxFactory.SingletonList<XmlNodeSyntax>(
                                    SyntaxFactory.XmlText()
                                    .WithTextTokens(
                                        SyntaxFactory.TokenList(
                                            SyntaxFactory.XmlTextLiteral(
                                                SyntaxFactory.TriviaList(),
                                                "Parametr s hodnotou " + param + ".",
                                                "Parametr s hodnotou " + param + ".",
                                                SyntaxFactory.TriviaList())))))
                            .WithStartTag(
                                SyntaxFactory.XmlElementStartTag(
                                    SyntaxFactory.XmlName(
                                        SyntaxFactory.Identifier("param")))
                                .WithAttributes(
                                    SyntaxFactory.SingletonList<XmlAttributeSyntax>(
                                        SyntaxFactory.XmlTextAttribute(
                                            SyntaxFactory.XmlName(
                                                SyntaxFactory.Identifier(" name=\""+param+"\"")),
                                            SyntaxFactory.MissingToken(SyntaxKind.DoubleQuoteToken),
                                            SyntaxFactory.MissingToken(SyntaxKind.DoubleQuoteToken))
                                        .WithEqualsToken(
                                            SyntaxFactory.MissingToken(SyntaxKind.EqualsToken)))))
                            .WithEndTag(
                                SyntaxFactory.XmlElementEndTag(
                                    SyntaxFactory.XmlName(
                                        SyntaxFactory.Identifier("param")))),
                            SyntaxFactory.XmlText()
                            .WithTextTokens(
                                SyntaxFactory.TokenList(
                                    SyntaxFactory.XmlTextNewLine(
                                        SyntaxFactory.TriviaList(),
                                        "\r\n",
                                        "\r\n",
                                        SyntaxFactory.TriviaList())))}))));
                                result.AddRange(code);
                            }
                            break;
                        }
                        else
                        {
                            foreach (var param in paramArray)
                            {
                                var code = SyntaxFactory.TriviaList(
                        SyntaxFactory.Trivia(
                            SyntaxFactory.DocumentationCommentTrivia(
                                SyntaxKind.SingleLineDocumentationCommentTrivia,
                                SyntaxFactory.List<XmlNodeSyntax>(
                                    new XmlNodeSyntax[]{
                            SyntaxFactory.XmlText()
                            .WithTextTokens(
                                SyntaxFactory.TokenList(
                                    SyntaxFactory.XmlTextLiteral(
                                        SyntaxFactory.TriviaList(
                                            SyntaxFactory.DocumentationCommentExterior(whiteSpace + "///")),
                                        " ",
                                        " ",
                                        SyntaxFactory.TriviaList()))),
                            SyntaxFactory.XmlExampleElement(
                                SyntaxFactory.SingletonList<XmlNodeSyntax>(
                                    SyntaxFactory.XmlText()
                                    .WithTextTokens(
                                        SyntaxFactory.TokenList(
                                            SyntaxFactory.XmlTextLiteral(
                                                SyntaxFactory.TriviaList(),
                                                inside,
                                                inside,
                                                SyntaxFactory.TriviaList())))))
                            .WithStartTag(
                                SyntaxFactory.XmlElementStartTag(
                                    SyntaxFactory.XmlName(
                                        SyntaxFactory.Identifier("param")))
                                .WithAttributes(
                                    SyntaxFactory.SingletonList<XmlAttributeSyntax>(
                                        SyntaxFactory.XmlTextAttribute(
                                            SyntaxFactory.XmlName(
                                                SyntaxFactory.Identifier(" name=\""+param+"\"")),
                                            SyntaxFactory.MissingToken(SyntaxKind.DoubleQuoteToken),
                                            SyntaxFactory.MissingToken(SyntaxKind.DoubleQuoteToken))
                                        .WithEqualsToken(
                                            SyntaxFactory.MissingToken(SyntaxKind.EqualsToken)))))
                            .WithEndTag(
                                SyntaxFactory.XmlElementEndTag(
                                    SyntaxFactory.XmlName(
                                        SyntaxFactory.Identifier("param")))),
                            SyntaxFactory.XmlText()
                            .WithTextTokens(
                                SyntaxFactory.TokenList(
                                    SyntaxFactory.XmlTextNewLine(
                                        SyntaxFactory.TriviaList(),
                                        "\r\n",
                                        "\r\n",
                                        SyntaxFactory.TriviaList())))}))));
                                result.AddRange(code);
                            }
                            break;
                        }
                    }
                case DocumentationType.Result:
                    {
                        var code = SyntaxFactory.TriviaList(
            SyntaxFactory.Trivia(
                SyntaxFactory.DocumentationCommentTrivia(
                    SyntaxKind.SingleLineDocumentationCommentTrivia,
                    SyntaxFactory.List<XmlNodeSyntax>(
                        new XmlNodeSyntax[]{
                            SyntaxFactory.XmlText()
                            .WithTextTokens(
                                SyntaxFactory.TokenList(
                                    SyntaxFactory.XmlTextLiteral(
                                        SyntaxFactory.TriviaList(
                                            SyntaxFactory.DocumentationCommentExterior(whiteSpace + "///")),
                                        " ",
                                        " ",
                                        SyntaxFactory.TriviaList()))),
                            SyntaxFactory.XmlExampleElement(
                                SyntaxFactory.SingletonList<XmlNodeSyntax>(
                                    SyntaxFactory.XmlText()
                                    .WithTextTokens(
                                        SyntaxFactory.TokenList(
                                            SyntaxFactory.XmlTextLiteral(
                                                SyntaxFactory.TriviaList(),
                                                inside,
                                                inside,
                                                SyntaxFactory.TriviaList())))))
                            .WithStartTag(
                                SyntaxFactory.XmlElementStartTag(
                                    SyntaxFactory.XmlName(
                                        SyntaxFactory.Identifier("returns"))))
                            .WithEndTag(
                                SyntaxFactory.XmlElementEndTag(
                                    SyntaxFactory.XmlName(
                                        SyntaxFactory.Identifier("returns")))),
                            SyntaxFactory.XmlText()
                            .WithTextTokens(
                                SyntaxFactory.TokenList(
                                    SyntaxFactory.XmlTextNewLine(
                                        SyntaxFactory.TriviaList(),
                                        Environment.NewLine,
                                        Environment.NewLine,
                                        SyntaxFactory.TriviaList())))}))));
                        result.AddRange(code);
                        break;
                    }
            }
            return result;
        }

        public enum DocumentationType
        {
            Summary = 1,
            Parameter,
            Result
        }
             
    }
}
