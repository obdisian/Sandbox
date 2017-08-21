# coding: utf-8

u"""
    サポートベクターマシンによる機械学習
    """

import numpy as np
from sklearn.svm import LinearSVC

# 学習データの読み込み
raw_training = np.loadtxt("data/auth.txt", delimiter=" ")
data_training = [[x[0], x[1]] for x in raw_training]
label_training = [x[2] for x in raw_training]

# 試験データの読み込み
data_test = np.loadtxt("data/mycoins.txt", delimiter=" ")

# サポートベクターマシンで学習
estimator = LinearSVC(C=1.0)
estimator.fit(data_training, label_training)

# 予測
label_prediction = estimator.predict(data_test)

# 結果出力
np.savetxt("data/answer.txt", label_prediction)

for i in range(len(label_prediction)):
    print("volume:{0:>6}   weight:{1:>6}   label:{2:>6}".format(data_test[i][0], data_test[i][1], label_prediction[i]))
