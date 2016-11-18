# 以琳·签到接口文档

@(【以琳】网络事工)

-------------------
 
- **文档作者** ：**Jamin.Huang**
- **创建日期** ：**2016年11月**
- **当前版本** ：**V1.0**

-------------------

- [目录](#-以琳·签到接口文档)
	- [1 前言](#1-前言)
		- [1.1 文档目的](#11-文档目的)
		- [1.2 接口说明](#12-接口说明)
	- [2 相关接口](#2-相关接口)
		- [2.1 聚会签到相关接口](#21-聚会签到相关接口)
			- [2.1.1 签到](#211-签到)
			- [2.1.2 签到列表](#212-签到列表)
			- [2.1.3 删除签到](#213-删除签到)
			- [2.1.4 获取签到人数](#214-获取签到人数)
			- [2.1.5 获取签到人员名单](#215-获取签到人员名单)
	- [3 附录](#3-附录)
		- [3.1 异常代码说明](#31-异常代码说明)

-------------------

## 1 前言

### 1.1 文档目的

>本文档适用于以琳·网络事工组有关签到API接口的相关接口文档。
>本文档主要针对以琳弟兄姊妹使用，用于事工拓展与相关介绍。

###  1.2 接口说明

- `[接口地址]`**http://120.27.201.161:7080/**


## 2 相关接口

### 2.1 聚会签到相关接口

#### 2.1.1 签到

**接口地址**: `URL/api/Gather/Sign`

**Input**

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
userName| 姓名 | String | 必填 |
groupName | 小组 | String |
gatherType | 聚会形式| Int | 必填 | 0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会

```
{
	"userName":"小明",
	"groupName":"",
	"gatherType":0
}
```

**Output**

字段名 | 字段 | 数据类型 | 是否必要 | 描述及要求
--- | --- | --- | --- | ---
Code | 状态码 | Integer | 必要 | 状态码 |
Content | 返回对象 | Object | 必要 | 
ErrorMsg | 错误信息 | String | 必要 |
ServerTime | 服务器时间 | Long | 必要 | 时间戳 |

**Content**

字段名 | 字段 | 数据类型 | 描述 |
--- | --- | --- | --- | ---
GatherId | 签到标识 | String | 唯一标识 |
UserName | 姓名 | String |
GroupName | 小组 | String |
GatherType | 聚会形式 | String | 0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会 |
IpAddress | 签到IP | String |
SignTime | 签到时间 | String |

**Success**

```
{
    "Code": 8200,
    "Content": {
        "GatherId": "9e8bb37d768fe3a8",
        "UserName": "小明",
        "GroupName": "",
        "GatherType": 0,
        "IpAddress": "115.238.66.206",
        "SignTime": "2016-11-18 09:39:00"
    },
    "ErrorMsg": "",
    "ServerTime": 1479433140188
}
```

**Fail**

```
{
  "Code": 8600,
  "Content": "",
  "ErrorMsg": "缺少参数",
  "ServerTime": 1479433189039
}
```

#### 2.1.2 签到列表

**接口地址**: `URL/api/Gather/ListByDate`

**Input**

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
userName| 关键字 | String | 必填 | 模糊查询
groupName | 小组 | String | 必填 | 空为获取全部小组
gatherType | 聚会形式| Int | 必填 | 0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会
date | 日期 | String | | 空为获取全部时间，`如果传date，则表示按日期传`
startTime | 开始时间 | String | | 
endTime | 结束时间 | String | |

```
{
	"userName":"",
	"groupName":"",
	"gatherType":0,
	"date":"2016-11-16"
}
```

**Output**

字段名 | 字段 | 数据类型 | 是否必要 | 描述及要求
--- | --- | --- | --- | ---
Code | 状态码 | Integer | 必要 | 状态码 |
Content | 返回对象 | Object | 必要 | 返回对象为列表
ErrorMsg | 错误信息 | String | 必要 |
ServerTime | 服务器时间 | Long | 必要 | 时间戳 |

**Content**

字段名 | 字段 | 数据类型 | 描述 |
--- | --- | --- | --- | ---
GatherId | 签到标识 | String | 唯一标识 |
UserName | 姓名 | String |
GroupName | 小组 | String |
GatherType | 聚会形式 | String | 0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会 |
IpAddress | 签到IP | String |
SignTime | 签到时间 | String |

**Success**

```
{
    "Code": 8200,
    "Content": [
        {
            "GatherId": "4e1f53f646dbb1d",
            "UserName": "刘乐慧",
            "GroupName": "以诺组",
            "GatherType": 0,
            "IpAddress": "112.10.109.96",
            "SignTime": "2016-11-16 22:44:35"
        },
        {
            "GatherId": "c4981184dcc0acf9",
            "UserName": "张欣悦",
            "GroupName": "以诺组",
            "GatherType": 0,
            "IpAddress": "115.238.66.206",
            "SignTime": "2016-11-16 18:12:20"
        }
    ],
    "ErrorMsg": "",
    "ServerTime": 1479433503172
}
```

#### 2.1.3 删除签到

**接口地址**: `URL/api/gather/Delete`

**Input**

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
gatherId | 签到标识 | String | 必填 | 唯一标识


```
{
	"gatherId":"a8977139d73cc118"
}
```

**Output**

字段名 | 字段 | 数据类型 | 是否必要 | 描述及要求
--- | --- | --- | --- | ---
Code | 状态码 | Integer | 必要 | 状态码 |
Content | 返回对象 | Object | 必要 | 成功返回为空字符串
ErrorMsg | 错误信息 | String | 必要 |
ServerTime | 服务器时间 | Long | 必要 | 时间戳 |

**Success**

```
{
  "Code": 8200,
  "Content": "",
  "ErrorMsg": "",
  "ServerTime": 1479433983690
}
```

#### 2.1.4 获取签到人数

**接口地址**: `URL/api/gather/getSignCount`

**Input**

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
gatherId | 签到标识 | String | 必填 | 唯一标识
startTime | 开始时间 | String | | 
endTime | 结束时间 | String | |

```
{
	"gatherType":0
}
```

**Output**

字段名 | 字段 | 数据类型 | 是否必要 | 描述及要求
--- | --- | --- | --- | ---
Code | 状态码 | Integer | 必要 | 状态码 |
Content | 返回对象 | Object | 必要 | 返回对象为数字
ErrorMsg | 错误信息 | String | 必要 |
ServerTime | 服务器时间 | Long | 必要 | 时间戳 |

**Success**

```
{
  "Code": 8200,
  "Content": 1,
  "ErrorMsg": "",
  "ServerTime": 1479434179948
}
```

#### 2.1.5 获取签到人员名单

**接口地址**: `URL/api/gather/getSignNameList`

**Input**

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
gatherType | 聚会形式 | String | 必填 | 0:主日聚会;1:学生小组聚会;2:毕业人生小组聚会;3:祷告会
date| 日期 | String | 必填 | 

```
{
	"gatherType":0,
	"date":"2016-11-16"
}
```

**Output**

字段名 | 字段 | 数据类型 | 是否必要 | 描述及要求
--- | --- | --- | --- | ---
Code | 状态码 | Integer | 必要 | 状态码 |
Content | 返回对象 | Object | 必要 | 返回对象为名单列表
ErrorMsg | 错误信息 | String | 必要 |
ServerTime | 服务器时间 | Long | 必要 | 时间戳 |

**Success**

```
{
    "Code": 8200,
    "Content": [
        "刘乐慧",
        "张欣悦"
    ],
    "ErrorMsg": "",
    "ServerTime": 1479434316153
}
```

## 3 附录

### 3.1 异常代码说明

代码标识 | 错误名称 | 详细意义 |
--- | --- | --- |
|8200|Success|操作成功|
|8300|UserNotExist|用户不存在|
|8301|UserInvalid|用户无效|
|8302|TokenInvalid|Token无效|
|8303|UserNameError|姓名格式不正确|
|8304|MobileError|手机号码格式不正确|
|8305|NameOrPwdError|用户名或密码错误|
|8306|VCodeError|验证码错误|
|8400|IpAddressError|IP地址错误|
|8400|NotFound|未找到资源|
|8500|ServerInternalError|服务内部错误|
|8600|MissParam|缺少参数|
|8700|ParamValueInvalid|参数值无效|
|8800|ResDataIsEmpty|接口返回对象为空|
|8900|EncryptInvalid|AES加密数据获取失败|
