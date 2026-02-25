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
import java.util.concurrent.ConcurrentHashMap;

public class YPayActivity extends ComponentActivity {

    private static final String YPAY_BUTTON = "YPay Button";
    private static final String ON_RESULT = "OnResult";
    private static final String PAYMENT_URL = "paymentUrl";
    private static final String PAYMENT_SESSION_KEY = "paymentSessionKey";
    private static ConcurrentHashMap<String, PaymentSession> paymentSessions = new ConcurrentHashMap<String, PaymentSession>();
    private static ConcurrentHashMap<String, YPayActivityResultListener> resultListeners = new ConcurrentHashMap<String, YPayActivityResultListener>();

    public static void savePaymentSession(String sessionKey, PaymentSession paymentSession) {
        paymentSessions.put(sessionKey, paymentSession);
    }

    public static boolean isPaymentSessionExists(String sessionKey) {
        return paymentSessions.containsKey(sessionKey);
    }

    public static PaymentSession getPaymentSession(String sessionKey) {
        if (!isPaymentSessionExists(sessionKey)) {
            return null;
        }
        return paymentSessions.get(sessionKey);
    }

    public static void removePaymentSession(String sessionKey) {
        paymentSessions.remove(sessionKey);
    }

    public static void clearPaymentSessions() {
        paymentSessions.clear();
    }

    public static void saveResultListener(String sessionKey, YPayActivityResultListener resultListener) {
        resultListeners.put(sessionKey, resultListener);
    }

    public static boolean isResultListenerExists(String sessionKey) {
        return resultListeners.containsKey(sessionKey);
    }

    public static void clearResultListeners() {
        resultListeners.clear();
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        String paymentSessionKey = getIntent().getStringExtra(PAYMENT_SESSION_KEY);
        if (paymentSessionKey == null || !isPaymentSessionExists(paymentSessionKey)) {
            finishWithResult(paymentSessionKey, new YPayResult.Failure("incorrect payment session key", null));
            return;
        }

        PaymentSession paymentSession = paymentSessions.get(paymentSessionKey);
        if (paymentSession == null) {
            finishWithResult(paymentSessionKey, new YPayResult.Failure("incorrect payment session key", null));
            return;
        }

        String paymentUrl = getIntent().getStringExtra(PAYMENT_URL);
        if (paymentUrl == null) {
            finishWithResult(paymentSessionKey, new YPayResult.Failure("incorrect payment url", null));
            return;
        }

        YPayLauncher launcher = new YPayLauncher(this, result -> {
            finishWithResult(paymentSessionKey, result);
        });
        PaymentData paymentData = new PaymentData.PaymentUrlFlowData(paymentUrl, null);
        YPayContractParams params = new YPayContractParams(paymentSession, paymentData);

        launcher.launch(params);
    }

    private void finishWithResult(String sessionKey, YPayResult result) {
        String message = resultToString(result);
        if (isResultListenerExists(sessionKey)) {
            resultListeners.get(sessionKey).OnResult(message);

            // We delete listener, because it is set right before the start of payment and cannot be reused
            resultListeners.remove(sessionKey);
        }
        finish();
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
