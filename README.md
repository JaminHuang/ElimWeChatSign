# 以琳·签到接口文档

@(【以琳】网络事工)

-------------------
 
- **文档作者** ：**Jamin.Huang**
- **创建日期** ：**2016年11月**
- **当前版本** ：**V1.0**

-------------------

- [目录](#-以琳·签到接口文档)
	- [0 修改记录](#0-修改记录)
	- [1 前言](#1-前言)
		- [1.1 文档目的](#11-文档目的)
		- [1.2 接口说明](#12-接口说明)
		- [1.3 参数说明](#13-参数说明)
			- [1.3.1 参数加密](#131-参数加密)
			- [1.3.2 密钥与向量](#132-密钥与向量)
	- [2 网页端相关接口](#2-网页端相关接口)
		- [2.1 聚会签到](#21-聚会签到)
			- [2.1.1 签到](#211-签到)
			- [2.1.2 签到列表](#212-签到列表)
			- [2.1.3 删除签到](#213-删除签到)
			- [2.1.4 获取签到人数](#214-获取签到人数)
			- [2.1.5 获取签到人员名单](#215-获取签到人员名单)
	- [3 移动端相关接口](#3-移动端相关接口)
		- [3.1 用户相关](#31-用户相关)
			- [3.1.1 获取验证码](#311-获取验证码)
			- [3.1.2 注册](#312-注册)
			- [3.1.3 登录](#313-登录)
			- [3.1.4 修改密码](#314-修改密码)
			- [3.1.5 重置密码](#315-重置密码)
			- [3.1.6 修改用户信息](#316-修改用户信息)
			- [3.1.7 获取用户信息](#317-获取用户信息)
			- [3.1.8 获取用户列表](#318-获取用户列表)
			- [3.1.9 退出登录](#319-退出登录)
		- [3.2 用户计划](#32-用户计划)
			- [3.2.1 添加计划](#321-添加计划)
			- [3.2.2 修改计划](#322-修改计划)
			- [3.2.3 获取指定用户的计划列表](#323-获取指定用户的计划列表)
			- [3.2.4 获取指定姓名的计划列表](#324-获取指定姓名的计划列表)
	- [4 附录](#4-附录)
		- [4.1 异常代码说明](#41-异常代码说明)

-------------------

## 0 修改记录

序号 | 版本 | 修改时间 | 修改记录 | 修改人员
--- | --- | --- | --- | ---
1 | V1.0 | 2016.10.24 | 项目整理，添加Core，发布接口 | JaminHuang
2 | V1.0 | 2016.10.31 | 添加签到内容 | JaminHuang
3 | V1.0 | 2016.11.10 | API项目框架重构 | JaminHuang
4 | V1.0 | 2016.11.11 | 添加用户相关接口[APP接口] | JaminHuang
5 | V1.0 | 2016.11.14 | 添加聚会签到接口 | JaminHuang
6 | V1.0 | 2016.11.15 | 添加跨域问题，加密 | JaminHuang
7 | V1.0 | 2016.11.18 | 整理接口文档 | JaminHuang
8 | V1.1 | 2016.12.01 | 添加移动端接口 | JaminHuang

## 1 前言

### 1.1 文档目的

>本文档适用于以琳·网络事工组有关签到API接口的相关接口文档。

###  1.2 接口说明

- `[接口地址]`**http://120.27.201.161:7080/**

### 1.3 参数说明

- `[必传参数]`**聚会签到相关接口不用必传参数**

#### 1.3.1 参数加密

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
body| 参数名 | String | 必填 | 参数使用AES加密，`AES加密后的密文`

#### 1.3.2 密钥与向量

密钥 | 对应向量
--- | ---
elimusic--server | { 0xC, 1, 0xB, 3, 0x5B, 0xD, 5, 4, 0xF, 7, 9, 0x17, 1, 0xA, 6, 8 }
elimusic--client | { 0x38, 0x31, 0x37, 0x34, 0x36, 0x33, 0x35, 0x33, 0x32, 0x31, 0x34, 0x38, 0x37, 0x36, 0x35, 0x32 }

## 2 网页端相关接口

>为方便了解接口，本文下方例举的请求接口的入参全部都以明文的方式

### 2.1 聚会签到

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

## 3 移动端相关接口

>为方便了解接口，本文下方例举的请求接口的入参全部都以明文的方式

 `****下方请求例举的接口入参，都需要添加下方的必传参数****`

字段名 | 字段 | 数据类型 | 描述及要求
--- | --- | --- | --- | ---
os | 操作系统 | String | Android，IOS，Server 
osVersion | 操作系统版本号 | String |
appVersion | App版本号 | String |
deviceId | 设备号 | String |
deviceToken | 友盟推送授权Token | String | 友盟推送授权Token，若无填空字符串
loginIp | 设备IP | String |
authToken | 登录授权Token | String | 除登录/注册等接口外，必填

### 3.1 用户相关

#### 3.1.1 获取验证码

**接口地址**: `URL/api/UserInfo/SendVCode`

**Input**

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
mobile | 手机号码 | String | 必填 |

```
{
	"mobile":"18868808315"
}
```

**Output**

字段名 | 字段 | 数据类型 | 是否必要 | 描述及要求
--- | --- | --- | --- | ---
Code | 状态码 | Integer | 必要 | 状态码 |
Content | 返回对象 | Object | 必要 | 成功返回验证码
ErrorMsg | 错误信息 | String | 必要 |
ServerTime | 服务器时间 | Long | 必要 | 时间戳 |

**Success**

```
{
    "Code": 8200,
    "Content": "866741",
    "ErrorMsg": "",
    "ServerTime": 1480570542520
}
```

#### 3.1.2 注册

**接口地址**: `URL/api/UserInfo/Reg`

**Input**

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
mobile | 手机号码 | String | 必填 |
password | 密码 | String | 必填 |
userName | 姓名 | String | 必填 |
vCode | 验证码 | String | 必填 |

```
{
	"mobile":"18868808315",
	"password":"123456",
	"userName":"小明",
	"vCode":"866741"
}
```

**Output**

字段名 | 字段 | 数据类型 | 是否必要 | 描述及要求
--- | --- | --- | --- | ---
Code | 状态码 | Integer | 必要 | 状态码 |
Content | 返回对象 | Object | 必要 | 成功返回空字符串
ErrorMsg | 错误信息 | String | 必要 |
ServerTime | 服务器时间 | Long | 必要 | 时间戳 |

**Success**

```
{
    "Code": 8200,
    "Content": "",
    "ErrorMsg": "",
    "ServerTime": 1480571288900
}
```

#### 3.1.3 登录

**接口地址**: `URL/api/UserInfo/Login`

**Input**

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
mobile | 手机号码 | String | 必填 |
password | 密码 | String | 必填 |

```
{
	"mobile":"18868808315",
	"password":"123456"
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
UserId | 用户标识 | String | 唯一标识 |
UserName | 姓名 | String |
Mobile | 手机号码 | String |
Avatar | 头像 | String | 头像地址
Gender | 性别 | Int | 0:女;1:男; |
Email | 邮箱 | String |
UserType | 用户类型 | Int| 0:普通;1:同工;9999:管理员
Os | 操作系统 | String |
OsVersion | 操作系统版本 | String |
DeviceId | 设备号 | String |
AppVersion | APP版本号 | String |
DeviceToken | 友盟推送标识 | String |
AuthToken | 登录授权Token | String |
UpdateTime | 最后修改时间 | String |

**Success**

```
{
    "Code": 8200,
    "Content": {
        "UserId": "71f59247a3a0714f",
        "UserName": "小明",
        "Mobile": "18868808315",
        "Avatar": "",
        "Gender": 1,
        "Email": "",
        "UserType": 0,
        "Os": "Server",
        "OsVersion": "1.0",
        "DeviceId": "",
        "AppVersion": "1.0",
        "DeviceToken": "",
        "AuthToken": "c49fd1106b30-621",
        "UpdateTime": "2016-12-01 14:07:13"
    },
    "ErrorMsg": "",
    "ServerTime": 1480572432815
}
```

#### 3.1.4 修改密码

**接口地址**: `URL/api/UserInfo/UpdatePassword`

**Input**

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
mobile | 手机号码 | String | 必填 |
password | 密码 | String | 必填 |

```
{
	"mobile":"18868808315",
	"oldPassword":"123456",
	"newPassword":"654321"
}
```

**Output**

字段名 | 字段 | 数据类型 | 是否必要 | 描述及要求
--- | --- | --- | --- | ---
Code | 状态码 | Integer | 必要 | 状态码 |
Content | 返回对象 | Object | 必要 | 成功返回空字符串
ErrorMsg | 错误信息 | String | 必要 |
ServerTime | 服务器时间 | Long | 必要 | 时间戳 |

**Success**

```
{
    "Code": 8200,
    "Content": "",
    "ErrorMsg": "",
    "ServerTime": 1480574092952
}
```

#### 3.1.5 重置密码

**接口地址**: `URL/api/UserInfo/ResetPassword`

**Input**

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
mobile | 手机号码 | String | 必填 |
password | 新密码 | String | 必填 |
vCode | 短信验证码 | String | 必填 |

```
{
	"mobile":"18868808315",
	"password":"654321",
	"vCode":"866741"
}
```

**Output**

字段名 | 字段 | 数据类型 | 是否必要 | 描述及要求
--- | --- | --- | --- | ---
Code | 状态码 | Integer | 必要 | 状态码 |
Content | 返回对象 | Object | 必要 | 成功返回空字符串
ErrorMsg | 错误信息 | String | 必要 |
ServerTime | 服务器时间 | Long | 必要 | 时间戳 |

**Success**

```
{
    "Code": 8200,
    "Content": "",
    "ErrorMsg": "",
    "ServerTime": 1480574092952
}
```

#### 3.1.6 修改用户信息

**接口地址**: `URL/api/UserInfo/Update`

**Input**

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
userId | 用户标识 | String | 必填 |
userName | 姓名 | String | 必填 |
mobile | 手机号码 | String | 必填 |
avatar | 头像地址 | String | 必填 |
gender | 性别 | Int | 必填 | 0:女;1:男; |
email | 邮箱 | String | 必填 |
userType | 用户类型 | Int | 必填 |0:普通;1:同工;9999:管理员

```
{
	"userId": "cbd411f866b32bcf",
    "userName": "小明",
    "mobile": "18868808315",
    "avatar": "",
    "gender": 1,
    "email": "",
    "userType": 0
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
UserId | 用户标识 | String | 唯一标识 |
UserName | 姓名 | String |
Mobile | 手机号码 | String |
Avatar | 头像 | String | 头像地址
Gender | 性别 | Int | 0:女;1:男; |
Email | 邮箱 | String |
UserType | 用户类型 | Int| 0:普通;1:同工;9999:管理员
UpdateTime | 最后修改时间 | String |

**Success**

```
{
    "Code": 8200,
    "Content": {
        "UserId": "cbd411f866b32bcf",
        "UserName": "小明",
        "Mobile": "18868808315",
        "Avatar": "",
        "Gender": 0,
        "Email": "",
        "UserType": 0,
        "UpdateTime": "2016-12-01 15:03:15"
    },
    "ErrorMsg": "",
    "ServerTime": 1480575795515
}
```

#### 3.1.7 获取用户信息

**接口地址**: `URL/api/UserInfo/Get`

**Input**

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
userId | 用户标识 | String | 必填 |

```
{
	"userId": "cbd411f866b32bcf"
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
UserId | 用户标识 | String | 唯一标识 |
UserName | 姓名 | String |
Mobile | 手机号码 | String |
Avatar | 头像 | String | 头像地址
Gender | 性别 | Int | 0:女;1:男; |
Email | 邮箱 | String |
UserType | 用户类型 | Int| 0:普通;1:同工;9999:管理员
UpdateTime | 最后修改时间 | String |

**Success**

```
{
    "Code": 8200,
    "Content": {
        "UserId": "cbd411f866b32bcf",
        "UserName": "小明",
        "Mobile": "18868808315",
        "Avatar": "",
        "Gender": 1,
        "Email": "",
        "UserType": 0,
        "UpdateTime": "2016-12-01 15:03:16"
    },
    "ErrorMsg": "",
    "ServerTime": 1480576311652
}
```

#### 3.1.8 获取用户列表

**接口地址**: `URL/api/UserInfo/List`

**Input**

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
userName | 姓名 | String | 必填 |

```
{
	"userName": ""
}
```

**Output**

字段名 | 字段 | 数据类型 | 是否必要 | 描述及要求
--- | --- | --- | --- | ---
Code | 状态码 | Integer | 必要 | 状态码 |
Content | 返回对象 | Object | 必要 | 成功返回用户列表
ErrorMsg | 错误信息 | String | 必要 |
ServerTime | 服务器时间 | Long | 必要 | 时间戳 |

**Content**

字段名 | 字段 | 数据类型 | 描述 |
--- | --- | --- | --- | ---
UserId | 用户标识 | String | 唯一标识 |
UserName | 姓名 | String |
Mobile | 手机号码 | String |
Avatar | 头像 | String | 头像地址
Gender | 性别 | Int | 0:女;1:男; |
Email | 邮箱 | String |
UserType | 用户类型 | Int| 0:普通;1:同工;9999:管理员
UpdateTime | 最后修改时间 | String |

**Success**

```
{
    "Code": 8200,
    "Content": [
        {
            "UserId": "cbd411f866b32bcf",
            "UserName": "小明",
            "Mobile": "18868808315",
            "Avatar": "",
            "Gender": 1,
            "Email": "",
            "UserType": 0,
            "UpdateTime": "2016-12-01 15:03:16"
        }
    ],
    "ErrorMsg": "",
    "ServerTime": 1480576501096
}
```

#### 3.1.9 退出登录

**接口地址**: `URL/api/UserInfo/ExitLogin`

**Input**

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
authToken | 授权Token | String | 必填 |

```
{
	"authToken": "7ee0a1104660-621"
}
```

**Output**

字段名 | 字段 | 数据类型 | 是否必要 | 描述及要求
--- | --- | --- | --- | ---
Code | 状态码 | Integer | 必要 | 状态码 |
Content | 返回对象 | Object | 必要 | 成功返回空字符串
ErrorMsg | 错误信息 | String | 必要 |
ServerTime | 服务器时间 | Long | 必要 | 时间戳 |

**Success**

```
{
    "Code": 8200,
    "Content": "",
    "ErrorMsg": "",
    "ServerTime": 1480576986339
}
```

### 3.2 用户计划

#### 3.2.1 添加计划

**接口地址**: `URL/api/UserPlan/Add`

**Input**

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
userId | 用户标识 | String | 必填 |
biblePlan | 读经计划 | String | 必填 |
bookPlan | 读书计划 | String | 必填 |
startDate | 开始时间 | String | 必填 |
endDate | 结束时间 | String | 必填 |

```
{
	"userId":"cbd411f866b32bcf",
	"biblePlan":"plan1",
	"bookPlan":"plan2",
	"startDate":"2016-12-01",
	"endDate":"2016-12-07"
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
PlanId | 计划标识 | String | 唯一标识，主键
UserId | 用户标识 | String | 唯一标识
BiblePlan | 读经计划 | String |
BookPlan | 读书计划 | String | 头像地址
StartDate | 开始时间 | String | yyyy-MM-dd HH:mm:ss
EndDate | 结束时间 | String | yyyy-MM-dd HH:mm:ss
UpdateTime | 最后修改时间 | String |

**Success**

```
{
    "Code": 8200,
    "Content": {
        "PlanId": "725c6680545c8dfd",
        "UserId": "cbd411f866b32bcf",
        "BiblePlan": "plan1",
        "BookPlan": "plan2",
        "StartDate": "2016-12-01 00:00:00",
        "EndDate": "2016-12-07 00:00:00",
        "UpdateTime": "2016-12-01 15:52:25"
    },
    "ErrorMsg": "",
    "ServerTime": 1480578745333
}
```

#### 3.2.2 修改计划

**接口地址**: `URL/api/UserPlan/Update`

**Input**

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
planId | 计划标识 | String | 必填 |
userId | 用户标识 | String | 必填 |
biblePlan | 读经计划 | String | 必填 |
bookPlan | 读书计划 | String | 必填 |
startDate | 开始时间 | String |
endDate | 结束时间 | String |

```
{
	"planId":"725c6680545c8dfd",
	"userId":"cbd411f866b32bcf",
	"biblePlan":"biblePlan",
	"bookPlan":"bookPlan",
	"startDate":"2016-12-01",
	"endDate":"2016-12-08"
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
PlanId | 计划标识 | String | 唯一标识，主键
UserId | 用户标识 | String | 唯一标识
BiblePlan | 读经计划 | String |
BookPlan | 读书计划 | String | 头像地址
StartDate | 开始时间 | String | yyyy-MM-dd HH:mm:ss
EndDate | 结束时间 | String | yyyy-MM-dd HH:mm:ss
UpdateTime | 最后修改时间 | String |

**Success**

```
{
    "Code": 8200,
    "Content": {
        "PlanId": "725c6680545c8dfd",
        "UserId": "cbd411f866b32bcf",
        "BiblePlan": "biblePlan",
        "BookPlan": "bookPlan",
        "StartDate": "2016-12-01 00:00:00",
        "EndDate": "2016-12-08 00:00:00",
        "UpdateTime": "2016-12-01 16:08:52"
    },
    "ErrorMsg": "",
    "ServerTime": 1480579732739
}
```

#### 3.2.3 获取指定用户的计划列表

**接口地址**: `URL/api/UserPlan/Update`

**Input**

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
userId | 用户标识 | String | 必填 |
startDate | 开始时间 | String | | 不传则默认无开始时间
endDate | 结束时间 | String | | 不传则默认为当前时间

```
{
	"userId":"cbd411f866b32bcf",
	"startDate":"2016-12-01",
	"endDate":"2016-12-09"
}
```

**Output**

字段名 | 字段 | 数据类型 | 是否必要 | 描述及要求
--- | --- | --- | --- | ---
Code | 状态码 | Integer | 必要 | 状态码 |
Content | 返回对象 | Object | 必要 | 成功返回计划列表
ErrorMsg | 错误信息 | String | 必要 |
ServerTime | 服务器时间 | Long | 必要 | 时间戳 |

**Content**

字段名 | 字段 | 数据类型 | 描述 |
--- | --- | --- | --- | ---
PlanId | 计划标识 | String | 唯一标识，主键
UserId | 用户标识 | String | 唯一标识
BiblePlan | 读经计划 | String |
BookPlan | 读书计划 | String | 头像地址
StartDate | 开始时间 | String | yyyy-MM-dd HH:mm:ss
EndDate | 结束时间 | String | yyyy-MM-dd HH:mm:ss
UpdateTime | 最后修改时间 | String |

**Success**

```
{
    "Code": 8200,
    "Content": [
        {
            "PlanId": "4a832cc39682163c",
            "UserId": "cbd411f866b32bcf",
            "BiblePlan": "plan1",
            "BookPlan": "plan2",
            "StartDate": "2016-12-01 00:00:00",
            "EndDate": "2016-12-06 00:00:00",
            "UpdateTime": "2016-12-01 16:10:42"
        },
        {
            "PlanId": "725c6680545c8dfd",
            "UserId": "cbd411f866b32bcf",
            "BiblePlan": "biblePlan",
            "BookPlan": "bookPlan",
            "StartDate": "2016-12-01 00:00:00",
            "EndDate": "2016-12-08 00:00:00",
            "UpdateTime": "2016-12-01 16:08:53"
        }
    ],
    "ErrorMsg": "",
    "ServerTime": 1480579876881
}
```

#### 3.2.4 获取指定姓名的计划列表

**接口地址**: `URL/api/UserPlan/List`

**Input**

字段名 | 字段 | 数据类型 | 是否必填 | 描述及要求
--- | --- | --- | --- | ---
userName | 用户姓名 | String | 必填 |
startDate | 开始时间 | String | | 不传则默认无开始时间
endDate | 结束时间 | String | | 不传则默认为当前时间

```
{
	"userName":"小明",
	"startDate":"2016-12-01",
	"endDate":"2016-12-09"
}
```

**Output**

字段名 | 字段 | 数据类型 | 是否必要 | 描述及要求
--- | --- | --- | --- | ---
Code | 状态码 | Integer | 必要 | 状态码 |
Content | 返回对象 | Object | 必要 | 成功返回计划列表
ErrorMsg | 错误信息 | String | 必要 |
ServerTime | 服务器时间 | Long | 必要 | 时间戳 |

**Content**

字段名 | 字段 | 数据类型 | 描述 |
--- | --- | --- | --- | ---
PlanId | 计划标识 | String | 唯一标识，主键
UserId | 用户标识 | String | 唯一标识
BiblePlan | 读经计划 | String |
BookPlan | 读书计划 | String | 头像地址
StartDate | 开始时间 | String | yyyy-MM-dd HH:mm:ss
EndDate | 结束时间 | String | yyyy-MM-dd HH:mm:ss
UpdateTime | 最后修改时间 | String |

**Success**

```
{
    "Code": 8200,
    "Content": [
        {
            "PlanId": "4a832cc39682163c",
            "UserId": "cbd411f866b32bcf",
            "BiblePlan": "plan1",
            "BookPlan": "plan2",
            "StartDate": "2016-12-01 00:00:00",
            "EndDate": "2016-12-06 00:00:00",
            "UpdateTime": "2016-12-01 16:10:42"
        },
        {
            "PlanId": "725c6680545c8dfd",
            "UserId": "cbd411f866b32bcf",
            "BiblePlan": "biblePlan",
            "BookPlan": "bookPlan",
            "StartDate": "2016-12-01 00:00:00",
            "EndDate": "2016-12-08 00:00:00",
            "UpdateTime": "2016-12-01 16:08:53"
        }
    ],
    "ErrorMsg": "",
    "ServerTime": 1480579876881
}
```

## 4 附录

### 4.1 异常代码说明

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
