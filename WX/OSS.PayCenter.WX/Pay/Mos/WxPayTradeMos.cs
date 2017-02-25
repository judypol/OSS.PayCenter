﻿#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：微信支付模快 —— 交易实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-24
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using OSS.Common.Extention;

namespace OSS.PayCenter.WX.Pay.Mos
{
    #region  查询订单接口
    public class WxQueryOrderReq : WxPayBaseReq
    {
        /// <summary>   
        ///    微信订单号 二选一 String(32) 微信的订单号，建议优先使用
        /// </summary>  
        public string transaction_id { get; set; }

        /// <summary>   
        ///    商户订单号 String(32) 20150806125346
        /// </summary>  
        public string out_trade_no { get; set; }

        /// <summary>
        ///  设置参与加密的字段
        /// </summary>
        protected override void SetSignDics()
        {
            SetDicItem("transaction_id", transaction_id);
            SetDicItem("out_trade_no", out_trade_no);
        }
    }

    /// <summary>
    ///  查询订单响应实体
    /// </summary>
    public class WxQueryOrderResp : WxPayBaseResp
    {
        /// <summary>   
        ///    设备号 可空 String(32) 微信支付分配的终端设备号，
        /// </summary>  
        public string device_info { get; set; }

        /// <summary>   
        ///    用户标识 必填 String(128) 用户在商户appid下的唯一标识
        /// </summary>  
        public string openid { get; set; }

        /// <summary>   
        ///    是否关注公众账号 可空 String(1) 用户是否关注公众账号，Y-关注，N-未关注，仅在公众账号类型支付有效
        /// </summary>  
        public string is_subscribe { get; set; }

        /// <summary>   
        ///    交易类型 必填 String(16) 调用接口提交的交易类型，取值如下：JSAPI，NATIVE，APP，MICROPAY，详细说明见参数规定
        /// </summary>  
        public string trade_type { get; set; }

        /// <summary>   
        ///    交易状态 必填 String(32) SUCCESS—支付成功,REFUND—转入退款,NOTPAY—未支付,CLOSED—已关闭,REVOKED—已撤销（刷卡支付）,USERPAYING--用户支付中,PAYERROR--支付失败(其他原因，如银行返回失败)
        /// </summary>  
        public string trade_state { get; set; }

        /// <summary>   
        ///    付款银行 必填 String(16) 银行类型，采用字符串类型的银行标识
        /// </summary>  
        public string bank_type { get; set; }

        /// <summary>   
        ///    标价金额 必填 Int 订单总金额，单位为分
        /// </summary>  
        public int total_fee { get; set; }

        /// <summary>   
        ///    应结订单金额 可空 Int 应结订单金额=订单金额-非充值代金券金额，应结订单金额小于等于订单金额。
        /// </summary>  
        public int settlement_total_fee { get; set; }

        /// <summary>   
        ///    标价币种 可空 String(8) 货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY，其他值列表详见货币类型
        /// </summary>  
        public string fee_type { get; set; }

        /// <summary>   
        ///    现金支付金额 必填 Int 现金支付金额订单现金支付金额，详见支付金额
        /// </summary>  
        public int cash_fee { get; set; }

        /// <summary>   
        ///    现金支付币种 可空 String(16) 货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY，其他值列表详见货币类型
        /// </summary>  
        public string cash_fee_type { get; set; }

        /// <summary>   
        ///    代金券金额 可空 Int “代金券”金额小于订单金额，订单金额-“代金券”金额=现金支付金额，详见支付金额
        /// </summary>  
        public int coupon_fee { get; set; }

        /// <summary>   
        ///    代金券使用数量 可空 Int 代金券使用数量
        /// </summary>  
        public int coupon_count { get; set; }

        /// <summary>   
        ///    微信支付订单号 必填 String(32) 微信支付订单号
        /// </summary>  
        public string transaction_id { get; set; }

        /// <summary>   
        ///    商户订单号 必填 String(32) 商户系统的订单号，与请求一致。
        /// </summary>  
        public string out_trade_no { get; set; }

        /// <summary>   
        ///    附加数据 可空 String(128) 附加数据，原样返回
        /// </summary>  
        public string attach { get; set; }

        /// <summary>   
        ///    支付完成时间 必填 String(14) 订单支付时间，格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010。其他详见时间规则
        /// </summary>  
        public string time_end { get; set; }

        /// <summary>   
        ///    交易状态描述 必填 String(256) 对当前查询订单状态的描述和下一步操作的指引
        /// </summary>  
        public string trade_state_desc { get; set; }


        // 注意：这里代金券coupon_count>0 时，类型金额等可以通过 this["coupon_fee_下标"] 获取

        /// <summary>
        ///  订单中使用的优惠券列表
        /// </summary>
        public List<WxPayOrderCouponMo> coupons { get; set; }

        /// <summary>
        /// 格式化自身属性部分
        /// </summary>
        protected override void FormatPropertiesFromMsg()
        {
            device_info = this["device_info"];
            openid = this["openid"];
            is_subscribe = this["is_subscribe"];
            trade_type = this["trade_type"];
            trade_state = this["trade_state"];

            bank_type = this["bank_type"];
            total_fee = this["total_fee"].ToInt32();
            settlement_total_fee = this["settlement_total_fee"].ToInt32();
            fee_type = this["fee_type"];
            cash_fee = this["cash_fee"].ToInt32();

            cash_fee_type = this["cash_fee_type"];
            coupon_fee = this["coupon_fee"].ToInt32();
            coupon_count = this["coupon_count"].ToInt32();
            transaction_id = this["transaction_id"];
            out_trade_no = this["out_trade_no"];

            attach = this["attach"];
            time_end = this["time_end"];
            trade_state_desc = this["trade_state_desc"];

            if (coupon_count > 0)
            {
                coupons = new List<WxPayOrderCouponMo>(coupon_count);
                for (int i = 0; i < coupon_count; i++)
                {
                    var coupon = new WxPayOrderCouponMo();
                    coupon.coupon_fee = this["coupon_fee_" + i].ToInt32();
                    coupon.coupon_id = this["coupon_id_" + i];
                    coupon.coupon_type = this["coupon_type_" + i];
                }
            }
        }
    }

    /// <summary>
    ///   支付订单对应的代金券实体
    /// </summary>
    public class WxPayOrderCouponMo
    {
        /// <summary>   
        ///    代金券类型 可空 String CASH--充值代金券 NO_CASH---非充值代金券订单使用代金券时有返回（取值：CASH、NO_CASH）。$n为下标,从0开始编号，举例：coupon_type_$0
        /// </summary>  
        public string coupon_type { get; set; }

        /// <summary>   
        ///    代金券ID 可空 String(20) 代金券ID, $n为下标，从0开始编号
        /// </summary>  
        public string coupon_id { get; set; }

        /// <summary>   
        ///    单个代金券支付金额 可空 Int 单个代金券支付金额, $n为下标，从0开始编号
        /// </summary>  
        public int coupon_fee { get; set; }
    }

    #endregion


    #region 关闭订单实体

    public class WxPayCloseOrderReq:WxPayBaseReq
    {
        /// <summary>
        ///    商户订单号 必填 String(32) 商户系统内部的订单号
        /// </summary>  
        public string out_trade_no { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void SetSignDics()
        {
            SetDicItem("out_trade_no", out_trade_no);
        }
    }

    #endregion

    #region  退款实体
    /// <summary>
    ///  请求退款实体
    /// </summary>
    public class WxPayRefundReq:WxPayBaseReq
    {
        /// <summary>   
        ///    设备号 可空 String(32) 终端设备号
        /// </summary>  
        public string device_info { get; set; }

        /// <summary>   
        ///    微信订单号 和商户订单号二选一 String(28) 微信生成的订单号，在支付通知中有返回
        /// </summary>  
        public string transaction_id { get; set; }

        /// <summary>   
        ///    商户订单号 String(32) 
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>   
        ///    商户退款单号 必填 String(32) 商户系统内部的退款单号，商户系统内部唯一，同一退款单号多次请求只退一笔
        /// </summary>  
        public string out_refund_no { get; set; }

        /// <summary>   
        ///    订单金额 必填 Int 订单总金额，单位为分，只能为整数，详见支付金额
        /// </summary>  
        public int total_fee { get; set; }

        /// <summary>   
        ///    退款金额 必填 Int 退款总金额，订单总金额，单位为分，只能为整数，详见支付金额
        /// </summary>  
        public int refund_fee { get; set; }

        /// <summary>   
        ///    货币种类 可空 String(8) 货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY，其他值列表详见货币类型
        /// </summary>  
        public string refund_fee_type { get; set; }

        /// <summary>   
        ///    操作员 必填 String(32) 操作员帐号, 默认为商户号
        /// </summary>  
        public string op_user_id { get; set; }

        /// <summary>   
        ///    退款资金来源 可空 String(30) 仅针对老资金流商户使用 REFUND_SOURCE_UNSETTLED_FUNDS---未结算资金退款（默认使用未结算资金退款）REFUND_SOURCE_RECHARGE_FUNDS---可用余额退款
        /// </summary>  
        public string refund_account { get; set; }

        protected override void SetSignDics()
        {
           SetDicItem("device_info", device_info);
            SetDicItem("transaction_id", transaction_id);
            SetDicItem("out_trade_no", out_trade_no);
            SetDicItem("out_refund_no", out_refund_no);
            SetDicItem("total_fee", total_fee);

            SetDicItem("refund_fee", refund_fee);
            SetDicItem("refund_fee_type", refund_fee_type);
            SetDicItem("op_user_id", op_user_id);
            SetDicItem("refund_account", refund_account);


        }
    }

    public class WxPayRefundResp : WxPayBaseResp
    {
        /// <summary>   
        ///    设备号 可空 String(32) 终端设备号
        /// </summary>  
        public string device_info { get; set; }

        /// <summary>   
        ///    微信订单号 必填 String(28) 微信订单号
        /// </summary>  
        public string transaction_id { get; set; }

        /// <summary>   
        ///    商户订单号 必填 String(32) 商户系统内部的订单号
        /// </summary>  
        public string out_trade_no { get; set; }

        /// <summary>   
        ///    商户退款单号 必填 String(32) 商户退款单号
        /// </summary>  
        public string out_refund_no { get; set; }

        /// <summary>   
        ///    微信退款单号 必填 String(28) 微信退款单号
        /// </summary>  
        public string refund_id { get; set; }

        /// <summary>   
        ///    退款渠道 可空 String(16) ORIGINAL—原路退款 BALANCE—退回到余额
        /// </summary>  
        public string refund_channel { get; set; }

        /// <summary>   
        ///    退款金额 必填 Int 退款总金额,单位为分,可以做部分退款
        /// </summary>  
        public int refund_fee { get; set; }

        /// <summary>   
        ///    应结退款金额 可空 Int 去掉非充值代金券退款金额后的退款金额，退款金额=申请退款金额-非充值代金券退款金额，退款金额<=申请退款金额
        /// </summary>  
        public int settlement_refund_fee { get; set; }

        /// <summary>   
        ///    标价金额 必填 Int 订单总金额，单位为分，只能为整数，详见支付金额
        /// </summary>  
        public int total_fee { get; set; }

        /// <summary>   
        ///    应结订单金额 可空 Int 去掉非充值代金券金额后的订单总金额，应结订单金额=订单金额-非充值代金券金额，应结订单金额<=订单金额。
        /// </summary>  
        public int settlement_total_fee { get; set; }

        /// <summary>   
        ///    标价币种 可空 String(8) 订单金额货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY，其他值列表详见货币类型
        /// </summary>  
        public string fee_type { get; set; }

        /// <summary>   
        ///    现金支付金额 必填 Int 现金支付金额，单位为分，只能为整数，详见支付金额
        /// </summary>  
        public string cash_fee { get; set; }

        /// <summary>   
        ///    现金支付币种 可空 String(16) 货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY，其他值列表详见货币类型
        /// </summary>  
        public string cash_fee_type { get; set; }

        /// <summary>   
        ///    现金退款金额 可空 Int 现金退款金额，单位为分，只能为整数，详见支付金额
        /// </summary>  
        public string cash_refund_fee { get; set; }

        /// <summary>   
        ///    代金券退款总金额 可空 Int 代金券退款金额小于退款金额，退款金额-代金券或立减优惠退款金额为现金，说明详见代金券或立减优惠
        /// </summary>  
        public string coupon_refund_fee { get; set; }

        /// <summary>   
        ///    退款代金券使用数量 可空 Int 退款代金券使用数量
        /// </summary>  
        public int coupon_refund_count { get; set; }

        /// <summary>
        /// 退款代金券信息
        /// </summary>
        public List<WxPayOrderCouponMo> refund_coupons { get; set; }

        /// <summary>
        /// 格式化自身属性部分
        /// </summary>
        protected override void FormatPropertiesFromMsg()
        {
            device_info = this["device_info"];
            transaction_id = this["transaction_id"];
            out_trade_no = this["out_trade_no"];
            out_refund_no = this["out_refund_no"];
            refund_id = this["refund_id"];

            refund_channel = this["refund_channel"];
            refund_fee = this["refund_fee"].ToInt32();
            settlement_refund_fee = this["settlement_refund_fee"].ToInt32();
            total_fee = this["total_fee"].ToInt32();
            settlement_total_fee = this["settlement_total_fee"].ToInt32();

            fee_type = this["fee_type"];
            cash_fee = this["cash_fee"];
            cash_fee_type = this["cash_fee_type"];
            cash_refund_fee = this["cash_refund_fee"];
            coupon_refund_fee = this["coupon_refund_fee"];

            coupon_refund_count = this["coupon_refund_count"].ToInt32();
            if (coupon_refund_count > 0)
            {
                refund_coupons = new List<WxPayOrderCouponMo>(coupon_refund_count);
                for (int i = 0; i < coupon_refund_count; i++)
                {
                    var coupon = new WxPayOrderCouponMo();
                    coupon.coupon_fee = this["coupon_refund_fee_" + i].ToInt32();
                    coupon.coupon_id = this["coupon_refund_id_" + i];
                    coupon.coupon_type = this["coupon_type_" + i];
                }
            }
        }
    }



    #endregion

}