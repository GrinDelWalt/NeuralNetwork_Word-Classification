﻿using System;
using System.Diagnostics;
using NeuralNetwork.Core;
using NeuralNetwork.ServicesManager;
using NeuralNetwork.ServicesManager.Vectors;

namespace NeuralNetwork
{
    static class Program
    {
        private static FileManager _fileManager;

        static void Main(string[] args)
        {
            int trainStartCount = 13821;
            int trainEndCount = 16420;

            // Для блочного обучения указать:
            int startDataSetIndex = 296848;
            int endDataSetIndex = 306848;

            #region Set process settings

            Process thisProc = Process.GetCurrentProcess();
            thisProc.PriorityClass = ProcessPriorityClass.High;

            #endregion

            const int receptors = 75;

            const int numberOfOutputClasses = 1; // Количество наших классов
            int[] neuronByLayer = { 50, 50, numberOfOutputClasses };

            _fileManager = new FileManager();

            var networkTeacher = new NetworkTeacher(neuronByLayer, receptors, 13, _fileManager)
            {
                Iteration = trainEndCount,
                TestVectors = _fileManager.ReadVectors("inputDataTestPart_temp.txt")
            };

            //networkTeacher.PreparingLearningData(true);

            if(networkTeacher.CheckMemory())
            { 
                networkTeacher.TrainNet(startDataSetIndex, endDataSetIndex, trainStartCount);

                networkTeacher.CommonTestColorized();

                networkTeacher.Visualize();

                networkTeacher.PrintLearnStatistic(startDataSetIndex, endDataSetIndex, true);

                if (networkTeacher.CheckMemory())
                {
                    networkTeacher.BackupMemory();
                }
            }
            else
            {
                Console.WriteLine("Train failed!");
            }

            Console.ReadKey();
        }
    }
}
