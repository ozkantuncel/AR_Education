import { initializeApp } from 'https://www.gstatic.com/firebasejs/9.23.0/firebase-app.js';
import { getFirestore, doc, getDoc } from 'https://www.gstatic.com/firebasejs/9.23.0/firebase-firestore.js';

// Firebase yapılandırması (GÜVENLİK: Mümkünse sunucu tarafında yönetin)
const firebaseConfig = {
    apiKey: "AIzaSyDtMTWBPbK9YaB1JHx762f1y-d0UQys_do",
    authDomain: "cashierless-checkout.firebaseapp.com",
    projectId: "cashierless-checkout",
    storageBucket: "cashierless-checkout.firebasestorage.app",
    messagingSenderId: "432384240296",
    appId: "1:432384240296:android:bb834277f46311e908b09d"
};

// Firebase'i başlat
const app = initializeApp(firebaseConfig);
const db = getFirestore(app);

const statusMessage = document.getElementById('status-message');
const downloadLink = "https://drive.google.com/file/d/1QRdAp9yrLgw_J4FpOmKGDtYzCWQsWNK1/view?usp=sharing";

async function checkFirebaseStatus() {
    try {
        const docSnap = await getDoc(doc(db, 'ORDER', 'TR-GEMI'));
        return docSnap.exists() ? docSnap.data().isChecked : null;
    } catch (error) {
        console.error("Firestore'dan veri alınırken hata oluştu:", error);
        return null;
    }
}

async function updateUI() {
    const isChecked = await checkFirebaseStatus();
    console.log("Güncel Durum:", isChecked);

    if (isChecked === true) {
        window.location.href = downloadLink;
    } else if (isChecked === false) {
        statusMessage.textContent = "İndirme işlemi henüz onaylanmadı. Lütfen bekleyiniz veya ana sayfaya dönünüz.";
    } else {
        statusMessage.textContent = "Bağlantı veya veri sorunları. Lütfen ana sayfaya dönünüz.";
    }
}

document.addEventListener('DOMContentLoaded', () => {
    updateUI();
    setInterval(updateUI, 10000);
});