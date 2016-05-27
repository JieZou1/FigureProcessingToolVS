using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelErrorAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            //Panel.ObjectAnnotation.ListErrors(@"\Users\jie\Openi\Panel\data\Train\EvaluationJaylene.txt", @"C:\Users\jie\Openi\Panel\data\Train\Jaylene", @"C:\Users\jie\Openi\Panel\data\Train\Jaylene-Error");
            //Panel.ObjectAnnotation.ListErrors(@"\Users\jie\Openi\Panel\data\Train\EvaluationSantosh.txt", @"C:\Users\jie\Openi\Panel\data\Train\Santosh", @"C:\Users\jie\Openi\Panel\data\Train\Santosh-Error");

            Panel.ObjectAnnotation.CorrectComparison(@"\Users\jie\Openi\Panel\data\Train\EvaluationJaylene.txt", @"\Users\jie\Openi\Panel\data\Train\EvaluationSantosh.txt");
        }
    }
}
