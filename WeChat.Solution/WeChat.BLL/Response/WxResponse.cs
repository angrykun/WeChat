using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.Entity;
using WeChat.Enums;

namespace WeChat.BLL
{
    public class WxResponse : IWxResponse
    {
        Func<BaseRequestMessage, EnterParamEntity, string> responseDelegate = null;

        #region 响应消息
        /// <summary>
        /// 响应消息
        /// </summary>
        /// <param name="response">请求消息</param>
        /// <param name="param">请求参数</param>
        /// <returns></returns>
        public string Execute(BaseRequestMessage request, EnterParamEntity param)
        {
            string result = string.Empty;
            var requestType = (MsgTypeRequestEnum)Enum.Parse(typeof(MsgTypeRequestEnum), request.MsgType.ToUpperInvariant());
            BaseResponseMessage responseModel = null;
            switch (requestType)
            {
                //文本消息
                case MsgTypeRequestEnum.TEXT:
                    responseDelegate = CreateResponseMessageFromText;
                    result = responseDelegate(request, param);
                    break;
                #region MyRegion
                //图片消息
                case MsgTypeRequestEnum.IMAGE:
                    responseModel = new TextResponseMessage
                        {
                            FromUserName = request.ToUserName,
                            ToUserName = request.FromUserName,
                            MsgType = MsgTypeResponseEnum.TEXT.ToString().ToLower(),
                            Content = "您好，您发送了一张图片！"
                        };
                    result = MessageFactory.CreateResponseType(responseModel, param, MsgTypeResponseEnum.TEXT);
                    break;
                //视频消息
                case MsgTypeRequestEnum.VIDEO:
                    return "";
                //链接消息
                case MsgTypeRequestEnum.LINK:
                    return "";
                //地理消息
                case MsgTypeRequestEnum.LOCATION:
                    return "";
                //短视频消息
                case MsgTypeRequestEnum.SHORTVIDEO:
                    return "";
                //音频消息
                case MsgTypeRequestEnum.VOICE:
                    return "";
                default: return "";
                #endregion
            }
            return result;
        }
        #endregion


        #region 处理来自文本类消息
        /// <summary>
        ///  处理来自文本类消息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public string CreateResponseMessageFromText(BaseRequestMessage request, EnterParamEntity param)
        {
            string result = string.Empty;
            BaseResponseMessage responseModel = null;

            var requestModel = request as TextRequestMessage;
            if (requestModel.Content.Equals("您好"))
            {
                responseModel = new TextResponseMessage
                  {
                      FromUserName = request.ToUserName,
                      ToUserName = request.FromUserName,
                      MsgType = MsgTypeResponseEnum.TEXT.ToString().ToLower(),
                      Content = GetContent("")
                  };
                result = MessageFactory.CreateResponseType(responseModel, param, MsgTypeResponseEnum.TEXT);
            }
            else if (requestModel.Content.Equals("图文"))
            {
                responseModel = new NewsResponseMessage
                {
                    FromUserName = request.ToUserName,
                    ToUserName = request.FromUserName,
                    MsgType = MsgTypeResponseEnum.NEWS.ToString().ToLower(),
                    ArticleCount = "1",
                    Articles = new List<Article>
                    {
                      new Article{
                       Title ="测试图文标题1",
                        Description="测试图文描述",
                         PicUrl="http://hn5ery.natappfree.cc/Content/images/pic1.png",
                          Url="http://m.flm365.com/member/Index"
                      },
                      new Article{
                       Title ="测试图文标题2",
                        Description="测试图文描述2",
                         PicUrl="http://hn5ery.natappfree.cc/Content/images/img1.jpg",
                          Url="http://m.flm365.com/Member/Store"
                      },
                       new Article{
                       Title ="测试图文标题3",
                        Description="测试图文描述3",
                         PicUrl="http://hn5ery.natappfree.cc/Content/images/img2.jpg",
                          Url="http://m.flm365.com/Member/LoanList"
                      },
                    }
                };
                result = MessageFactory.CreateResponseType(responseModel, param, MsgTypeResponseEnum.NEWS);
            }
            else
            {
                responseModel = new TextResponseMessage
                {
                    FromUserName = request.ToUserName,
                    ToUserName = request.FromUserName,
                    MsgType = MsgTypeResponseEnum.TEXT.ToString().ToLower(),
                    Content = GetContent(requestModel.Content)
                };
                result = MessageFactory.CreateResponseType(responseModel, param, MsgTypeResponseEnum.TEXT);
            }


            return result;
        }
        #endregion


        #region 食物集合
        private static List<string> breakfaseList = new List<string>
        {
           "包子",
           "豆浆",
           "饭团",
           "八宝粥",
          "茶叶蛋",
          "杂粮煎饼"

        };

        private static List<string> lunchList = new List<string>
        {
           "麻辣烫",
           "干锅",
           "冒菜",
           "曼玲粥店",
          "新石器烤肉",
          "浏阳蒸菜",
          "牛蛙面"
        };

        private static List<string> dinnerList = new List<string>
        {
             "麻辣烫",
           "面条",
           "水饺",
           "馄饨",
          "酸辣米粉",
          "美味不用等",
          "老鸭粉丝汤" ,
          "校外酒家",
           "白素酸菜鱼",
           "盖浇饭",
          "正新鸡排"
        };

        #endregion

        public string GetContent(string id)
        {
            StringBuilder result = new StringBuilder();
            switch (id)
            {
                case "1":
                    result.AppendFormat("今天早饭吃{0}，怎么样？/酷", breakfaseList.OrderBy(_ => Guid.NewGuid()).First());
                    break;
                case "2":
                    result.AppendFormat("今天中饭吃{0}，怎么样？", lunchList.OrderBy(_ => Guid.NewGuid()).First());
                    break;
                case "3":
                    result.AppendFormat("今天晚饭吃{0}，怎么样？", dinnerList.OrderBy(_ => Guid.NewGuid()).First());
                    break;
                //case "4":
                //    result.AppendFormat("今天早饭吃{0}，怎么样？", breakfaseList.OrderBy(_ => Guid.NewGuid()).First());
                //    break;
                default:
                    result.AppendFormat("欢迎关注公众号，请选择：\n");
                    result.AppendFormat("【1】早饭吃什么？\n");
                    result.AppendFormat("【2】晚饭吃什么？\n");
                    result.AppendFormat("【3】晚饭吃什么？\n");
                    //result.AppendFormat("【4】今天上海天气？");
                    result.AppendFormat("想知道答案？请回复数字。");
                    break;
            }
            return result.ToString();
        }

    }
}
