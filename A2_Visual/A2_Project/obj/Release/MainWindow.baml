   M S B A M L   `   `   `  ���� E  AA2_Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:A2_Project
A2_Project      A2_Project.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:A2_Project       � Title$  
MainWindow��$	��668��$	��586��.��� $��0,-1,0,1q�.+��� -    ��Name$ String1TextBox��$
��Left=�$��23��$��
119,86,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��252��$��депозитор��$��14(�$��Qanelas)��� -   $ String2TextBox��$
��Left=�$��23��$��119,120,0,0q�$
 Wrap=�$	��Top=�$	��252��$ ��реализоваться��$��14(�$��Qanelas)��� -    ��Name$ label��$Y��SРедакционное расстояние Дамерау-Левенштейна��$
��Left=�$��	37,29,0,0q�$	��Top=�$	��485��$��18(�$��Qanelas)��� -    ��Name$ 	Calculate��$��Рассчитать��$
��Left=�$��395,162,0,0q�$	��Top=�$	��149��$��25�����	#FFFFF300$��Qanelas SemiBold)�$��14(��� -   $ LevenshteinAnswer��$��
=-number-=��$
��Left=�$��
43,209,0,0q�$	��Top=�$��Qanelas)�$��16(� K� ZIndex$ 2��$y�Hidden=��� -    ��Name$ scroll��$	��100��$
��Left=�$��37,517,0,21q�$
��Auto=�$
��Auto=�$	��474�����	#FFE6E6E6$y�Hidden=�.���� -   $ AnswerLabel��$	��Top=�$
��Auto��$
��Auto��$��
-=answer=-����. $
��Auto=�$��Qanelas Thin)� J� RenderTransformOrigin$ 0.461,0.583&�$y�Hidden=��� -   $ 
label_Copy��$��Строка 1:��$
��Left=�$��	42,84,0,0q�$	��Top=�$��77��$��14(�$��Qanelas Light)��� -	   $ label_Copy1��$��Строка 2:��$
��Left=�$��
42,118,0,0q�$	��Top=�$��77��$��14(�$��Qanelas Light)��� -
   $ SettingButton��$��Настройки��$
��Left=�$��
37,162,0,0q�$	��Top=�$	��149��$��25��$��Qanelas)����	#FF040404#��� ��$��14(��� -   $ LevenshteinAnswer_Copy��$&�� Подробный расчет:��$
��Left=�$��
37,487,0,0q�$	��Top=�$��Qanelas)�$��14(�$y�Hidden=��� -   $ VLabel1��$��Визуализация:��$
��Left=�$��
47,285,0,0q�$	��Top=�$��Qanelas Black Italic)�$��16(�$y�Hidden=��� -   $ string1current��$$��111111111111111111111111111111��$
��Left=�$��
47,327,0,0q�$	��Top=�$��Qanelas SemiBold)�$��22(�$	��492��$ 0.5,0.5&�$y�Hidden=��� -   $ string2current��$$��111111111111111111111111111111��$
��Left=�$��
47,367,0,0q�$	��Top=�$��Qanelas SemiBold)�$��22(�$	��492��$ 0.5,0.5&�$y�Hidden=��� -   $ StepBack��$	��< <��$
��Left=�$��
47,427,0,0q�$	��Top=�$��59��$��25��$��Qanelas SemiBold)����	#FF040404#��� ��$y�Hidden=��� -   $ 	StepFront��$	��> >��$
��Left=�$��113,427,0,0q�$	��Top=�$��59��$��25��$��Qanelas SemiBold)����	#FF040404#��� ��$y�Hidden=��� -   $ VLabel2��$��Шаг��$
��Left=�$��434,285,0,0q�$	��Top=�$��Qanelas Black Italic)�$��16(�$y�Hidden=��� -   $ currstep��$��10��$
��Left=�$��472,285,0,0q�$	��Top=�$��Qanelas Black Italic)�$��16(�$y�Hidden=��� -   $ 
answerstep��$��10��$
��Left=�$��508,285,0,0q�$	��Top=�$��Qanelas Black Italic)�$��16(�$y�Hidden=��� -   $ VLabel3��$��/��$
��Left=�$��493,285,0,0q�$	��Top=�$��Qanelas Black Italic)�$��16(�$y�Hidden=��� -    ��Name$ rect1�����	#FFABFBFF$
��Left=�$��68��$��
36,203,0,0q�
���   �$	��Top=�$	��508��$y�Hidden=��� -   $ rect2�����	#FFFFFCC5$
��Left=�$	��193��$��
37,276,0,0q�
���   �$	��Top=�$	��507��$ -1��$y�Hidden=��� -   $ 	FromFile1��$��Из файла��$
��Left=�$��
377,84,0,0q�$	��Top=�$��81��$��25��$��Qanelas)����	#FF040404#��� ��$��14(��� -   $ 	FromFile2��$��Из файла��$
��Left=�$��377,118,0,0q�$	��Top=�$��81��$��25��$��Qanelas)����	#FF040404#��� ��$��14(��� -   $ 
Dictionary��$��Словарь��$
��Left=�$��
463,84,0,0q�$	��Top=�$��81��$��59��$��Qanelas)����	#FF040404#��� ��$��14(��� -   $ 
MatrixOpen��$��Матрица��$
��Left=�$��434,427,0,0q�$	��Top=�$��88��$��25��$��Qanelas SemiBold)����	#FF040404#��� ��$y�Hidden=��� -   $ VErrorLabel��$9��3-= Визуализация невозможна =-��$
��Left=�$��172,347,0,0q�$	��Top=�$��Qanelas Light)�$��16(�$	��240��$ 0.5,0.5&�$��26��$y�Hidden=��� -   $ LevenshteinCriteria��$��=-criteria-=��$
��Left=�$��
43,235,0,0q�$	��Top=�$��Qanelas)�$��16(�$ 2��$y�Hidden=�