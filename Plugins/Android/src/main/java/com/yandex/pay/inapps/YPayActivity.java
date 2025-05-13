// Yandex Pay InApps Plugin.

package com.yandex.pay.inapps;

import android.os.Bundle;
import androidx.activity.ComponentActivity;
import com.yandex.pay.inapps.YPayLauncher;
import com.yandex.pay.inapps.PaymentSession;
import com.yandex.pay.inapps.YPayContractParams;
import com.yandex.pay.inapps.PaymentData;
import com.yandex.pay.inapps.YPayResult;
import com.unity3d.player.UnityPlayer;

public class YPayActivity extends ComponentActivity {

    private static final String YPAY_BUTTON = "YPay Button";
    private static final String ON_RESULT = "OnResult";
    private static final String PAYMENT_URL_KEY = "paymentUrl";
    private static PaymentSession _paymentSession;
    private static YPayActivityResultListener _resultListener;

    public static void setPaymentSession(PaymentSession paymentSession) {
        _paymentSession = paymentSession;
    }

    public static void clearPaymentSession() {
        _paymentSession = null;
    }

    public static void setResultListener(YPayActivityResultListener resultListener) {
        _resultListener = resultListener;
    }

    public static void clearResultListener() {
        _resultListener = null;
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        if (_paymentSession == null) {
            sendResult(new YPayResult.Failure("config data not provided", null));
            finish();
            return;
        }

        String paymentUrl = getIntent().getStringExtra(PAYMENT_URL_KEY);
        if (paymentUrl == null) {
            sendResult(new YPayResult.Failure("incorrect payment url", null));
            finish();
            return;
        }

        YPayLauncher launcher = new YPayLauncher(this, result -> {
            sendResult(result);
            finish();
        });
        PaymentData paymentData = new PaymentData.PaymentUrlFlowData(paymentUrl, null);
        YPayContractParams params = new YPayContractParams(_paymentSession, paymentData);

        launcher.launch(params);
    }

    private void sendResult(YPayResult result) {
        String message = resultToString(result);
        if (_resultListener != null) {
            _resultListener.OnResult(message);
        }
    }

    private String resultToString(YPayResult result) {
        if (result instanceof YPayResult.Success) {
            YPayResult.Success success = (YPayResult.Success)result;
            return String.format("Success {\"OrderId\": \"%s\"}", success.getOrderId().getValue());
        } else if (result instanceof YPayResult.Failure) {
            YPayResult.Failure failure = (YPayResult.Failure)result;
            return String.format("Failure {\"ErrorMessage\": \"%s\"}", failure.getErrorMsg());
        } else {
            return "Cancelled {}";
        }
    }
}
