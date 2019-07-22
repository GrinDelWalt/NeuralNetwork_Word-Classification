﻿using System.IO;

namespace NeuralNetwork
{
    public class FileManager
    {
        private string _dataPath;

        private FileManager() { }

        public FileManager(string dataPath)
        {
            _dataPath = dataPath;
        }

        public double[] LoadMemory(int layerNumber, int neuronNumber)
        {
            double[] memory = new double[0];

            using (StreamReader fileReader = new StreamReader(_dataPath))
            {
                while (!fileReader.EndOfStream)
                {
                    string[] readedLine = fileReader.ReadLine().Split(' ');

                    if ((readedLine[0] == "layer_" + layerNumber) && (readedLine[1] == "neuron_" + neuronNumber))
                    {
                        memory = GetWeights(readedLine);
                    }
                }
            }

            return memory;
        }

        private double[] GetWeights(string[] readedLine)
        {
            double[] weights = new double[readedLine.Length - 2];

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = double.Parse(readedLine[i + 2]);
            }

            return weights;
        }

        public void PrepareToSaveMemory()
        {
            File.Delete(_dataPath);
        }

        public void SaveMemory(int layerNumber, int neuronNumber, double[] weights)
        {
            using (StreamWriter fileWriter = new StreamWriter(_dataPath, true))
            {
                fileWriter.Write("layer_{0} neuron_{1}", layerNumber, neuronNumber);

                for (int i = 0; i < weights.Length; i++)
                {
                    fileWriter.Write(" " + weights[i]);
                }

                fileWriter.WriteLine("");
            }
        }

    }
}
