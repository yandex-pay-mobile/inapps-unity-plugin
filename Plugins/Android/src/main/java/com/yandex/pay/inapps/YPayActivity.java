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

    public static void setPaymentSession(PaymentSession paymentSession) {
        _paymentSession = paymentSession;
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        String paymentUrl = getIntent().getStringExtra(PAYMENT_URL_KEY);
        if (paymentUrl == null) {
            sendToUnity(new YPayResult.Failure("Payment URL is null", null));
            finish();
            return;
        }

        YPayLauncher launcher = new YPayLauncher(this, result -> {
            sendToUnity(result);
            finish();
        });
        PaymentData paymentData = new PaymentData.PaymentUrlFlowData(paymentUrl, null);
        YPayContractParams params = new YPayContractParams(_paymentSession, paymentData);

        launcher.launch(params);
    }

    private void sendToUnity(YPayResult result) {
        String message = resultToString(result);
        UnityPlayer.UnitySendMessage(YPAY_BUTTON, ON_RESULT, message);
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
