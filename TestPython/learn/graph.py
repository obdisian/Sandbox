# coding: utf-8

import numpy as np
import pandas as pd
import seaborn as sns


raw_training = np.loadtxt("data/auth.txt", delimiter = " ")
df_training = pd.DataFrame(raw_training, columns = ["volume", "weight", "label"])

# 近似線
sns.regplot("volume", "weight", data = df_training)
#sns.lmplot("volume", "weight", data = df_training, hue = "label", fit_reg = True)
sns.plt.show()
