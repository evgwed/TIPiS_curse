using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
namespace SimplexMethod
{
    class SimplexMet
    {
        //Основной метод, реализующий работу симплекс-алгоритма
        public static void Simplex(ref object[,] matrix)
        {
            SearchBase(matrix);
            RabotaSOgranich(ref matrix);
            RabotaSZeleZ(ref matrix);
        }
        //Посторение и поиск по базе
        private static void SearchBase(object[,] _matrix)
        {
            for (int i = 1; i < _matrix.GetLength(0); i++)
            {
                if ((string)_matrix[i, 0] == "x")
                {
                    for (int j = 2 ; j < _matrix.GetLength(1); j++)
                    {
                        if ((int)_matrix[i, j] == 1 && CheckNull(_matrix, i, j))
                        {
                            _matrix[i, 0] = _matrix[0, j];
                            break;
                        }
                    }
                }
            }
        }

        //проверка на нули в столбце
        private static bool CheckNull(object[,] _matrix, int _numRow, int _numCol)
        {
            if (((string)_matrix[0, _numCol]).Contains('x')) 
                return false;
            for (int i = _matrix.GetLength(0) - 1; i > 0; i--)
            {
                if (Convert.ToDouble(_matrix[i, _numCol]) == 0 || Convert.ToDouble(_matrix[_numRow, _numCol]) == 1)
                    continue;
                else
                    return false;
            }
            return true;
        }

        //Поиск числа искусственных переменных
        private static int CountIskustv(object[,] _matrix)
        {
            int artCounter = 0;

            for (int i = 0; i < _matrix.GetLength(1); i++)
                if ((_matrix[0, i] as string).Contains("art"))
                    artCounter++;

            return artCounter;
        }

        //Проверка на равенство 1-це исскуственных переменных, а остальных 0-лю 
        private static bool ProverkaStroki(object[,] _matrix)
        {
            for (int j = 2; j < _matrix.GetLength(1) - CountIskustv(_matrix); j++)
            {
                if (Convert.ToDouble(_matrix[_matrix.GetLength(0) - 1, j]) == 0)
                    continue;
                else
                    return false;
            }
            return true;
        }
        //переход от искусственной функции к целевой
        private static object[,] PerehodArray(ref object[,] _matrixBefore)
        {
            object[,] matrixAfter = new object[_matrixBefore.GetLength(0) - 1, _matrixBefore.GetLength(1) - CountIskustv(_matrixBefore)];

            for (int i = 0; i < matrixAfter.GetLength(0); i++)
                for (int j = 0; j < matrixAfter.GetLength(1); j++)
                    matrixAfter[i, j] = _matrixBefore[i, j];

            return matrixAfter;
        }

        //Отказ от искусственной переменной
        private static void RabotaSOgranich(ref object[,] _matrix)
        {
            while (!ProverkaStroki(_matrix))
            {
                Writing(_matrix);
                int j_min = PoiskVedushego(_matrix);
                int i_min = PoiskVedushei(_matrix, j_min, 2);
                Trace.WriteLine("Ведущим элементом был выбран элемено " + i_min + " строки и " + j_min+" столбца");
                Editbase(_matrix, i_min, j_min);
                PrivedenieKBasisu(_matrix, i_min, j_min);
                PoluchenieNullVStolbe(_matrix, i_min, j_min);
            }
            Writing(_matrix);
            _matrix = PerehodArray(ref _matrix);
            Trace.WriteLine("Отбрасывание искусственных переменных и искусственной функции W");
            Writing(_matrix);
        }
        //Проверка на нахождение решения
        private static bool CheckingTargetFunctionEls(object[,] _matrix)
        {
            for (int j = 2; j < _matrix.GetLength(1); j++)
            {
                if (Convert.ToDouble(_matrix[_matrix.GetLength(0) - 1, j]) < 0)
                    return false;
                else
                    continue;
            }
            return true;
        }

        //Ищем результат
        private static void RabotaSZeleZ(ref object[,] _matrix)
        {
            while (!CheckingTargetFunctionEls(_matrix))
            {
                Writing(_matrix);
                int j_min = PoiskVedushego(_matrix);
                int i_min = PoiskVedushei(_matrix, j_min, 1);
                Trace.WriteLine("Ведущий элемент строка " + i_min + " и столбец " + j_min);
                Editbase(_matrix, i_min, j_min);
                PrivedenieKBasisu(_matrix, i_min, j_min);
                PoluchenieNullVStolbe(_matrix, i_min, j_min);
            }
            Trace.WriteLine("Результат получился!");
            Writing(_matrix);
        }

        //поиск ведущего столбца в строке
        private static int PoiskVedushego(object[,] _matrix)
        {
            int min_j = 2; 
            for (int j = 2; j < _matrix.GetLength(1) - CountIskustv(_matrix); j++)
                if (Convert.ToDouble(_matrix[_matrix.GetLength(0) - 1, j]) < Convert.ToDouble(_matrix[_matrix.GetLength(0) - 1, min_j]))
                    min_j = j;
            return min_j;
        }

        //Поиск первого неотрицательного
        private static int PoiskMinZnach(object[,] _matrix, int _min_j, int _subtractValue)
        {
            for (int i = 1; i < _matrix.GetLength(0) - _subtractValue; i++)
            {
                if ((Convert.ToDouble(_matrix[i, _min_j]) > 0) && (Convert.ToDouble(_matrix[i, 1]) / Convert.ToDouble(_matrix[i, _min_j]) > 0))
                {
                    return i;
                }
            }
            return -1;
        }

        //Для поиска номера ведущей строки в матрице 
        private static int PoiskVedushei(object[,] _matrix, int _min_j, int _subtractValue)
        {
            int min_i = PoiskMinZnach(_matrix, _min_j, _subtractValue);
            double minDivisionRes = Convert.ToDouble(_matrix[min_i, 1]) / Convert.ToDouble(_matrix[min_i, _min_j]);
            if (_subtractValue == 2)
            {
                bool f_priority = false; 
                if (_matrix[min_i, 0].ToString().Contains("art"))
                    f_priority = true; 
                for (int i = 1; i < _matrix.GetLength(0) - _subtractValue; i++)
                {
                    if (_matrix[i, 0].ToString().Contains("art"))
                        f_priority = true; 
                    else
                        f_priority = false;

                    if ((Convert.ToDouble(_matrix[i, _min_j]) > 0) && (Convert.ToDouble(_matrix[i, 1]) / Convert.ToDouble(_matrix[i, _min_j]) < minDivisionRes) && (Convert.ToDouble(_matrix[i, 1]) / Convert.ToDouble(_matrix[i, _min_j]) > 0))
                    {
                        minDivisionRes = (Convert.ToDouble(_matrix[i, 1])) / (Convert.ToDouble(_matrix[i, _min_j]));
                        min_i = i;
                    }
                    else
                    {
                        if ((Convert.ToDouble(_matrix[i, _min_j]) > 0) && (Convert.ToDouble(_matrix[i, 1]) / Convert.ToDouble(_matrix[i, _min_j]) == minDivisionRes) && (Convert.ToDouble(_matrix[i, 1]) / Convert.ToDouble(_matrix[i, _min_j]) > 0) && f_priority)
                        {
                            minDivisionRes = (Convert.ToDouble(_matrix[i, 1])) / (Convert.ToDouble(_matrix[i, _min_j]));
                            min_i = i;
                        }
                    }
                }
            }
            else
            {
                for (int i = 1; i < _matrix.GetLength(0) - _subtractValue; i++)
                {
                    if ((Convert.ToDouble(_matrix[i, _min_j]) > 0) && (Convert.ToDouble(_matrix[i, 1]) / Convert.ToDouble(_matrix[i, _min_j]) < minDivisionRes) && (Convert.ToDouble(_matrix[i, 1]) / Convert.ToDouble(_matrix[i, _min_j]) > 0))
                    {
                        minDivisionRes = (Convert.ToDouble(_matrix[i, 1])) / (Convert.ToDouble(_matrix[i, _min_j]));
                        min_i = i;
                    }
                }
            }
            return min_i;
        }

        //Изменение базы
        private static void Editbase(object[,] _matrix, int _min_i, int _min_j)
        {
            Trace.Write("\nПеременная " + _matrix[_min_i, 0]);
            _matrix[_min_i, 0] = _matrix[0, _min_j];
            Trace.WriteLine(" заменена на " + _matrix[_min_i, 0]);
        }

        //Приведение к базису
        private static void PrivedenieKBasisu(object[,] _matrix, int _min_i, int _min_j)
        {
            if (Convert.ToDouble(_matrix[_min_i, _min_j]) != 1)
            {
                double devisor = Convert.ToDouble(_matrix[_min_i, _min_j]);
                for (int j = 1; j < _matrix.GetLength(1); j++)
                    _matrix[_min_i, j] = Convert.ToDouble(_matrix[_min_i, j]) / devisor;
            }
        }

        //Получаем нули в столбце
        private static void PoluchenieNullVStolbe(object[,] _matrix, int _min_i, int _min_j)
        {
            for (int i = 1; i < _matrix.GetLength(0); i++)
            {
                if (i != _min_i)
                {
                    double tmp = Convert.ToDouble(_matrix[i, _min_j]);
                    if (tmp != 0)
                    {
                        for (int j = 1; j < _matrix.GetLength(1); j++)
                            _matrix[i, j] = Convert.ToDouble(_matrix[i, j]) - tmp * Convert.ToDouble(_matrix[_min_i, j]);
                    }
                    else
                        continue;
                }
            }

        }

        //Вывод матрицы
        public static void Writing(object[,] _matrix)
        {
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                Trace.WriteLine("");
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    if (_matrix[i, j].ToString().Contains("E-"))
                        Trace.Write(String.Format("{0,20}", "0"));
                    else
                        Trace.Write(String.Format("{0,20}", _matrix[i, j]));
                }
            }
            Trace.WriteLine("");
        }

        private static int counterFectsNames = 0; 
        private static int counterArtsNames = 0;
        private static List<object[,]> addingArtificials = new List<object[,]>(); 
        //Получение знаков условий
        private static string[] GetConditionSigns(TCT[] arry, int _countConditions)
        {
            string[] conditionSigns = new string[_countConditions];

            for (int i = 0; i < arry.Length; i++)
            {
                conditionSigns[i] = arry[i].c.SelectedItem.ToString();

            }
            return conditionSigns;
        }

        //Получение коэффициентов условий
        private static double[,] GetConditionsCoefficients(TCT[] arry, int _countConditions, int _countVariables, string[] str_zele)
        {
            double[,] coefficientsMatrix = new double[_countConditions + 1, _countVariables + 1]; 
            string[] tmp_sarry1 = str_zele[0].Split(' ');
            coefficientsMatrix[0, 0] = Convert.ToDouble(tmp_sarry1[0]);
            coefficientsMatrix[0, 1] = Convert.ToDouble(tmp_sarry1[1]);
            coefficientsMatrix[0, 2] = Convert.ToDouble(tmp_sarry1[2]);
            coefficientsMatrix[0, 3] = Convert.ToDouble(str_zele[1]);
            for (int i = 1; i < arry.Length; i++)
            {
                string[] tmp_sarry = arry[i].t1.Text.Split(' ');
                coefficientsMatrix[i, 0] = Convert.ToDouble(tmp_sarry[0]);
                coefficientsMatrix[i, 1] = Convert.ToDouble(tmp_sarry[1]);
                coefficientsMatrix[i, 2] = Convert.ToDouble(tmp_sarry[2]);
                coefficientsMatrix[i, 3] = Convert.ToDouble(arry[i].t2.Text);
            }
            return coefficientsMatrix;
        }
        //Создание матрицы целевой функции
        public static object[,] CreateTargetFunctionMatrix(TCT[] arry, int _countConditions, int _countVariables, string[] str_zell)
        {
            double[,] conditionCoefficients = GetConditionsCoefficients(arry, _countConditions, _countVariables, str_zell);
            object[,] targetMatrix = new object[3, _countVariables];
            for (int j = 0; j < targetMatrix.GetLength(1); j++)
            {
                targetMatrix[0, j] = "x" + (j + 1);
                targetMatrix[1, j] = conditionCoefficients[0, j];
                targetMatrix[2, j] = 0;
            }
            return targetMatrix;
        }

        //Моделирование уравнений(не неравенств, т.к. сразу создаем фективные переменные) условий через матрицу
        public static void CreateEquationMatrix(TCT[] arry, int _countConditions, int _countVariables, ref List<object[,]> _conditionSimplexMatrixes, string[] str_zell)
        {
            string[] conditionSigns = GetConditionSigns(arry, _countConditions);
            double[,] conditionCoefficients = GetConditionsCoefficients(arry, _countConditions, _countVariables, str_zell); //получили коэффициенты при переменных
            bool f_DistributionSimplex = false;
            object[,] matrixItem = null;
            for (int k = 0; k < _countConditions; k++)
            {
                int runningJ = 0;
                if (conditionSigns[k] == "=")
                {
                    matrixItem = new object[3, _countVariables + 1];
                    runningJ = _countVariables + 1;
                    f_DistributionSimplex = true;
                }
                if (conditionSigns[k] == "=<" || conditionSigns[k] == ">=")
                {
                    matrixItem = new object[3, _countVariables + 2];
                    runningJ = _countVariables + 2;
                    f_DistributionSimplex = true;
                }

                for (int j = 0; j < runningJ; j++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (j >= _countVariables + 1)
                        {
                           if (i == 0)
                            {
                                string fectName = "fect" + (j + counterFectsNames); //прибавляем число элементов списка для поддержания порядка нумерации фективных переменных 
                                counterFectsNames++;
                                matrixItem[i, j] = fectName;
                            }
                            if (i == 1)
                            {
                                if (conditionSigns[k] == "=<")
                                    matrixItem[i, j] = 1;
                                if (conditionSigns[k] == ">=")
                                    matrixItem[i, j] = -1;
                            }
                            if (i == 2)
                                matrixItem[i, j] = 0;
                        }
                        else
                        {
                            if (i == 0)
                                matrixItem[i, j] = "x" + (j + 1);
                            if (i == 1)
                            {
                                if (j == _countVariables)
                                {
                                    matrixItem[0, j] = "Value"; //имя свободного члена
                                    matrixItem[i, j] = conditionCoefficients[k, j] * (-1);
                                }
                                else
                                    matrixItem[i, j] = conditionCoefficients[k, j];
                            }
                            if (i == 2)
                                matrixItem[i, j] = 0;
                        }
                    }
                }
                if (f_DistributionSimplex)
                    _conditionSimplexMatrixes.Add(matrixItem);
            }
        }

        //Проверка необходимости добавления искусственной переменной
        private static bool CheckArtificialAddition(object[,] _conditionItem)
        {
            for (int j = 0; j < _conditionItem.GetLength(1); j++)
            {
                if ((_conditionItem[0, j].ToString()).Contains("fect") && (int)_conditionItem[1, j] == -1)
                    return true;
                if ((_conditionItem[0, j].ToString()).Contains("fect") && (int)_conditionItem[1, j] == 1)
                    return false;
            }
            return true;
        }

        //Проверка на уже добавленность искусственной переменной
        private static bool CheckArtificialExistence(object[,] _conditionItem)
        {
            for (int j = 0; j < _conditionItem.GetLength(1); j++)
            {
                if ((_conditionItem[0, j].ToString()).Contains("art"))
                    return true;
            }
            return false;
        }
        //Добавление искусственных переменных к элементу списка
        private static void AddArtificialVariableToItem(List<object[,]> _conditionSimplexMatrixes, object[,] _conditionItem, int _countVariables)
        {
            if (CheckArtificialAddition(_conditionItem) && !CheckArtificialExistence(_conditionItem))
            {
                object[,] copyItem = new object[3, _conditionItem.GetLength(1) + 1];
                for (int j = 0; j < _conditionItem.GetLength(1); j++)
                {
                    for (int i = 0; i < _conditionItem.GetLength(0); i++)
                    {
                        copyItem[i, j] = _conditionItem[i, j];
                    }
                }
                copyItem[0, copyItem.GetLength(1) - 1] = "art" + (counterArtsNames + counterFectsNames + 1 + _countVariables); 
                counterArtsNames++; 
                copyItem[1, copyItem.GetLength(1) - 1] = 1; 
                copyItem[2, copyItem.GetLength(1) - 1] = 0; 
                addingArtificials.Add(copyItem);
            }
            else
                addingArtificials.Add(_conditionItem);
        }
        public static void AddArtificialVariables(ref List<object[,]> _conditionSimplexMatrixes, int _countVariables)
        {
            for (int i = 0; i < _conditionSimplexMatrixes.Count; i++)
                AddArtificialVariableToItem(_conditionSimplexMatrixes, _conditionSimplexMatrixes[i], _countVariables); 
            _conditionSimplexMatrixes = addingArtificials; 
        }
        private static List<object[,]> LookForConditionsWithArtificialVars(List<object[,]> _conditionSimplexMatrixes)
        {
            List<object[,]> conditionsWithArtVars = new List<object[,]>();
            for (int k = 0; k < _conditionSimplexMatrixes.Count; k++)
            {
                for (int j = _conditionSimplexMatrixes[k].GetLength(1) - 1; j >= 0; j--)
                {
                    if ((_conditionSimplexMatrixes[k][0, j].ToString()).Contains("art"))
                    {
                        conditionsWithArtVars.Add(_conditionSimplexMatrixes[k]);
                        break; 
                    }
                }
            }

            return conditionsWithArtVars;
        }
        //Выражаем искусственные переменные
        private static void ExpressArtifialVariables(ref List<object[,]> _conditionsWithArtVars)
        {
            for (int k = 0; k < _conditionsWithArtVars.Count; k++)
            {
                for (int j = 0; j < _conditionsWithArtVars[k].GetLength(1); j++)
                {
                    if (!((_conditionsWithArtVars[k][0, j].ToString()).Contains("art")))
                    {
                        int tmpBox = Convert.ToInt32(_conditionsWithArtVars[k][1, j]);
                        _conditionsWithArtVars[k][1, j] = 0;
                        _conditionsWithArtVars[k][2, j] = Convert.ToInt32(_conditionsWithArtVars[k][2, j]) - tmpBox;
                    }
                }
            }
        }

        //Ищем имена фективных переменных, участвующих в образовании искусственной ф-ии
        private static List<string> GetNamesFectiveVariables(List<object[,]> _conditionsWithArtVars)
        {
            List<string> actedFectiveVariables = new List<string>(); //задействованные фективные переменные

            for (int k = 0; k < _conditionsWithArtVars.Count; k++)
            {
                for (int j = 0; j < _conditionsWithArtVars[k].GetLength(1); j++)
                {
                    if ((_conditionsWithArtVars[k][0, j].ToString()).Contains("fect") && !actedFectiveVariables.Contains((_conditionsWithArtVars[k][0, j].ToString())))
                        actedFectiveVariables.Add(_conditionsWithArtVars[k][0, j].ToString());
                }
            }

            return actedFectiveVariables;
        }

        //Создаем и выражаем искусственную функцию
        private static object[,] ExpressArtificialFunction(List<object[,]> _conditionsWithArtVars, int _countVariables)
        {
            List<string> actedFectiveVariables = GetNamesFectiveVariables(_conditionsWithArtVars);
            object[,] artificialFunctionMatrix = new object[3, _countVariables + actedFectiveVariables.Count + 1];
            for (int n = 0; n < _countVariables + 1; n++)
            {
                for (int k = 0; k < _conditionsWithArtVars.Count; k++)
                {
                    if (k == 0)
                        artificialFunctionMatrix[0, n] = _conditionsWithArtVars[k][0, n];

                    artificialFunctionMatrix[1, n] = Convert.ToInt32(artificialFunctionMatrix[1, n]) + Convert.ToInt32(_conditionsWithArtVars[k][2, n]);
                    artificialFunctionMatrix[2, n] = 0; 
                }
            }
            for (int j = _countVariables + 1, listCounter = 0; j < artificialFunctionMatrix.GetLength(1); j++, listCounter++)
            {
                artificialFunctionMatrix[0, j] = actedFectiveVariables[listCounter];
                artificialFunctionMatrix[1, j] = 1;
                artificialFunctionMatrix[2, j] = 0;
            }
            return artificialFunctionMatrix;
        }

        //Перенесем значение (Value) в правую часть искусственной ф-ции(при этом не забываем учесть знак "-")
        private static object[,] ChangeValueSide(object[,] _artificialFunction)
        {
            for (int j = 0; j < _artificialFunction.GetLength(1); j++)
            {

                if (_artificialFunction[0, j].ToString() == "Value")
                {
                    _artificialFunction[2, j] = (-1) * Convert.ToInt32(_artificialFunction[1, j]);
                    _artificialFunction[1, j] = 0;
                }
            }

            return _artificialFunction;
        }
        //Выражание искусственной функции (поэтапно) 
        public static object[,] GetArtificialFunctionMatrix(List<object[,]> _conditionSimplexMatrixes, int _countVariables)
        {
            List<object[,]> conditionsWithArtVars = LookForConditionsWithArtificialVars(_conditionSimplexMatrixes);
            ExpressArtifialVariables(ref conditionsWithArtVars);
            return ChangeValueSide(ExpressArtificialFunction(conditionsWithArtVars, _countVariables));
        }

        //Заполним заголовок таблицы и первый столбец (базу)
        private static void FillTableTitle(ref object[,] _simplexTable, int _countVariables)
        {
            _simplexTable[0, 0] = "Base";
            _simplexTable[0, 1] = "Value";
            for (int j = 2; j < _countVariables + 2; j++)
            {
                _simplexTable[0, j] = "x" + (j - 1);
            }
            for (int j = _countVariables + 2; j < _countVariables + 2 + counterFectsNames; j++)
            {
                _simplexTable[0, j] = "fect" + (j - 1);
            }
            for (int j = _countVariables + 2 + counterFectsNames; j < _simplexTable.GetLength(1); j++)
            {
                _simplexTable[0, j] = "art" + (j - 1);
            }
        }

        //Формирование столбца "Base"
        private static void FillColumnBase(ref object[,] _simplexTable)
        {
            for (int i = 1; i < _simplexTable.GetLength(0) - 2; i++)
            {
                _simplexTable[i, 0] = "x";
            }

            _simplexTable[_simplexTable.GetLength(0) - 2, 0] = "-z";
            _simplexTable[_simplexTable.GetLength(0) - 1, 0] = "-w";
        }

        //Заполним симплекс-матрицу нулями
        private static void FillSimplexMatrixByNulls(ref object[,] _simplexTable)
        {
            for (int i = 1; i < _simplexTable.GetLength(0); i++)
                for (int j = 1; j < _simplexTable.GetLength(1); j++)
                    _simplexTable[i, j] = 0;
        }

        //Приводим все симплекс-условия 
        private static void SetUsualView(ref List<object[,]> _conditionSimplexMatrixes)
        {
            for (int k = 0; k < _conditionSimplexMatrixes.Count; k++)
            {
                for (int j = 0; j < _conditionSimplexMatrixes[k].GetLength(1); j++)
                {
                    if (_conditionSimplexMatrixes[k][0, j].ToString() != "Value")
                    {
                        if (Convert.ToInt32(_conditionSimplexMatrixes[k][1, j]) == 0)
                        {
                            int tmpVar = Convert.ToInt32(_conditionSimplexMatrixes[k][2, j]) * (-1);
                            _conditionSimplexMatrixes[k][2, j] = 0;
                            _conditionSimplexMatrixes[k][1, j] = tmpVar;
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(_conditionSimplexMatrixes[k][1, j]) != 0)
                        {
                            int tmpVar = Convert.ToInt32(_conditionSimplexMatrixes[k][1, j]) * (-1);
                            _conditionSimplexMatrixes[k][2, j] = tmpVar;
                            _conditionSimplexMatrixes[k][1, j] = 0;
                        }
                    }
                }
            }
        }

        //Заносим значение через поиск по имени 
        private static void SearchAndSetValue(object[,] _simplexTable, string _varName, int _value, int _rowIndex)
        {
            for (int j = 0; j < _simplexTable.GetLength(1); j++)
            {
                if (_simplexTable[0, j].ToString() == _varName)
                {
                    _simplexTable[_rowIndex, j] = _value;
                    break;
                }
            }
        }

        //Заполняем матрицу коэффициентами условий, целевой ф-ии, искусственной ф-ии 
        private static void FillMatrixByCoefficients(ref object[,] _simplexTable, object[,] _targetFunctionMatrix, List<object[,]> _conditionSimplexMatrixes, object[,] _artificialFunctionMatrix)
        {
            _conditionSimplexMatrixes.Add(_targetFunctionMatrix);
            _conditionSimplexMatrixes.Add(_artificialFunctionMatrix);
            for (int i = 1, listCounter = 0; i < _simplexTable.GetLength(0); i++, listCounter++)
            {
                for (int k = 0; k < _conditionSimplexMatrixes[listCounter].GetLength(1); k++)
                {
                    if (Convert.ToInt32(_conditionSimplexMatrixes[listCounter][1, k]) != 0)
                        SearchAndSetValue(_simplexTable, _conditionSimplexMatrixes[listCounter][0, k].ToString(), Convert.ToInt32(_conditionSimplexMatrixes[listCounter][1, k]), listCounter + 1);
                    else
                        SearchAndSetValue(_simplexTable, _conditionSimplexMatrixes[listCounter][0, k].ToString(), Convert.ToInt32(_conditionSimplexMatrixes[listCounter][2, k]), listCounter + 1);
                }
            }
            _conditionSimplexMatrixes.RemoveRange(_conditionSimplexMatrixes.Count - 2, 2);
        }

        //Формируем таблицу для симплекс-метода
        public static object[,] FormTableForSimplexMethod(object[,] _targetFunctionMatrix, List<object[,]> _conditionSimplexMatrixes, object[,] _artificialFunctionMatrix, int _countSimplexConditions, int _countVariables)
        {
            object[,] simplexTable = new object[_countSimplexConditions + 3, _countVariables + counterFectsNames + counterArtsNames + 2];
            FillTableTitle(ref simplexTable, _countVariables);
            FillColumnBase(ref simplexTable);
            FillSimplexMatrixByNulls(ref simplexTable);
            SetUsualView(ref _conditionSimplexMatrixes);
            FillMatrixByCoefficients(ref simplexTable, _targetFunctionMatrix, _conditionSimplexMatrixes, _artificialFunctionMatrix);
            return simplexTable;
        }

        //Забор значений из столбца Base полученного решения
        private static object[,] PolushenieBase(object[,] _simplexTable)
        {
            object[,] solutionValues = new object[2, _simplexTable.GetLength(0) - 1];
            for (int i = 1, counter = 0; i < _simplexTable.GetLength(0); i++, counter++)
            {
                solutionValues[0, counter] = _simplexTable[i, 0];
                solutionValues[1, counter] = _simplexTable[i, 1];
            }
            return solutionValues;
        }

        //Проверка на завершенность решения симплекс-алгоритмом
        private static string SimplexSolved(object[,] _solutionValues, object[,] _targetFunction)
        {
            object[,] flagArray = new object[2, _targetFunction.GetLength(1)];
            for (int j = 0; j < flagArray.GetLength(1); j++)
            {
                flagArray[0, j] = _targetFunction[0, j];
                flagArray[1, j] = 0; 
            }
            for (int i = 0; i < flagArray.GetLength(1); i++)
            {
                for (int j = 0; j < _solutionValues.GetLength(1); j++)
                {
                    if (flagArray[0, i].ToString() == _solutionValues[0, j].ToString())
                        flagArray[1, i] = 1;
                }
            }
            string unknownVariable = null;

            for (int j = 0; j < flagArray.GetLength(1); j++)
                if (Convert.ToInt32(flagArray[1, j]) == 0)
                    unknownVariable = flagArray[0, j].ToString();

            return unknownVariable;
        }
        //Установка значения
        private static void UstanovkaZnach(object[,] _findingMatrix, int _j, object[,] _solutionValues)
        {
            for (int k = 0; k < _solutionValues.GetLength(1); k++)
            {
                if (_findingMatrix[0, _j].ToString() == _solutionValues[0, k].ToString())
                {
                    _findingMatrix[1, _j] = _solutionValues[1, k];
                }
            }
        }

        //Формирование матрицы для расчета неизвестного значения
        private static object[,] SizdanieArry(object[,] _targetFunction, object[,] _solutionValues)
        {
            object[,] findingMatrix = new object[3, _targetFunction.GetLength(1) + 1]; 
            for (int j = 0; j < findingMatrix.GetLength(1); j++)
            {
                if (j == findingMatrix.GetLength(1) - 1)
                {
                    findingMatrix[0, j] = "z";
                    findingMatrix[1, j] = (-1) * Convert.ToDouble(_solutionValues[1, _solutionValues.GetLength(1) - 1]);
                    findingMatrix[2, j] = 0;
                }
                else
                {
                    findingMatrix[0, j] = _targetFunction[0, j];
                    UstanovkaZnach(findingMatrix, j, _solutionValues);
                    findingMatrix[2, j] = _targetFunction[1, j];
                }
            }
            for (int j = 0; j < findingMatrix.GetLength(1); j++)
            {
                if (findingMatrix[1, j] == null)
                    findingMatrix[1, j] = "?";
            }
            return findingMatrix;
        }

        //Поиск номера столбца с неизвестной переменной в матрице поиска
        private static int GetUnknownVarCol(object[,] _findingMatrix, string _unknownVariable)
        {
            for (int j = 0; j < _findingMatrix.GetLength(1); j++)
                if (_findingMatrix[0, j].ToString() == _unknownVariable)
                    return j;
            return -1;
        }

        //Поиск неизвестного значения
        private static void PoiskNeizvestnZnach(object[,] _findingMatrix, string _unknownVariable)
        {
            double value = Convert.ToDouble(_findingMatrix[1, _findingMatrix.GetLength(1) - 1]);
            int k = GetUnknownVarCol(_findingMatrix, _unknownVariable);

            for (int j = 0; j < _findingMatrix.GetLength(1); j++)
            {
                if (k != j)
                {
                    value -= (Convert.ToDouble(_findingMatrix[1, j]) * Convert.ToDouble(_findingMatrix[2, j]));
                }
                else
                    continue;
            }
            value = value / Convert.ToDouble(_findingMatrix[2, k]);
            for (int j = 0; j < _findingMatrix.GetLength(1); j++)
            {
                if (_findingMatrix[1, j].ToString() == "?")
                    _findingMatrix[1, j] = value;
            }
        }

        //расчет недостающего значения и запись его в матрицу
        private static object[,] RaschetNeResheniya(object[,] _simplexTable, object[,] _targetFunction)
        {
            object[,] solutionValues = PolushenieBase(_simplexTable);
            string unknownVariable = SimplexSolved(solutionValues, _targetFunction);
            object[,] findingMatrix = SizdanieArry(_targetFunction, solutionValues);

            if (SimplexSolved(solutionValues, _targetFunction) != null)
                PoiskNeizvestnZnach(findingMatrix, unknownVariable);

            Trace.WriteLine("Значения полученных переменных:");
            for (int i = 0; i < solutionValues.GetLength(0); i++)
            {
                Trace.WriteLine("");
                for (int j = 0; j < solutionValues.GetLength(1); j++)
                {
                    Trace.Write(String.Format("{0,16}", solutionValues[i, j]));
                }
            }

            return findingMatrix;
        }

        //Получить решение
        public static object[,] PoluchenieResheniya(object[,] _simplexTable, object[,] _targetFunction, ref double minVertex, string zele, Label lab)
        {
            object[,] solutionMatrix = new object[2, _targetFunction.GetLength(1)];
            object[,] findingMatrix = RaschetNeResheniya(_simplexTable, _targetFunction);
            for (int j = 0; j < solutionMatrix.GetLength(1); j++)
            {
                solutionMatrix[0, j] = findingMatrix[0, j];
                solutionMatrix[1, j] = findingMatrix[1, j];

                if (solutionMatrix[1, j].ToString().Contains("E-"))
                    solutionMatrix[1, j] = 0;
            }

            Trace.WriteLine("\nОтветы в матричном представлении:\n");
            for (int i = 0; i < findingMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < findingMatrix.GetLength(1); j++)
                {
                    Trace.Write(String.Format("{0,16}", findingMatrix[i, j]));
                }
                Trace.WriteLine("");
            }

            minVertex = Convert.ToDouble(findingMatrix[1, findingMatrix.GetLength(1) - 1]);
            lab.Text += "\n" + String.Format("{0,5}", "x1") + " = " + String.Format("{0,5}", (findingMatrix[1, 0].ToString().IndexOf('E') > 0) ? "0" : findingMatrix[1, 0])
                    + "\n" + String.Format("{0,5}", "x2") + " = " + String.Format("{0,5}", (findingMatrix[1, 1].ToString().IndexOf('E') > 0) ? "0" : findingMatrix[1, 1])
                    + "\n" + String.Format("{0,5}", "x3") + " = " + String.Format("{0,5}", (findingMatrix[1, 2].ToString().IndexOf('E') > 0) ? "0" : findingMatrix[1, 2])
                    + "\n" + String.Format("{0,5}", "Z(x)") + " = " + String.Format("{0,5}", minVertex);
            return solutionMatrix;
        }

    }
}
