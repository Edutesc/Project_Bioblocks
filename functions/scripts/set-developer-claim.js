/* eslint-disable no-console */
"use strict";

const admin = require("firebase-admin");

function parseEnabled(raw) {
  if (raw === undefined) return true;
  if (raw === "true") return true;
  if (raw === "false") return false;
  throw new Error("O segundo argumento deve ser true ou false.");
}

async function main() {
  const email = process.argv[2];
  const enabled = parseEnabled(process.argv[3]);
  const projectId = process.argv[4] ||
    process.env.FIREBASE_PROJECT_ID ||
    "microlearning-dev-79c0c";

  if (!email) {
    throw new Error(
        "Uso: npm run set-developer -- usuario@email.com " +
        "[true|false] [projectId]",
    );
  }

  admin.initializeApp({
    credential: admin.credential.applicationDefault(),
    projectId,
  });

  const auth = admin.auth();
  const user = await auth.getUserByEmail(email);
  const currentClaims = user.customClaims || {};
  const nextClaims = {...currentClaims};

  if (enabled) {
    nextClaims.developer = true;
  } else {
    delete nextClaims.developer;
  }

  await auth.setCustomUserClaims(user.uid, nextClaims);

  console.log(
      `Claim developer ${enabled ? "concedida" : "removida"} ` +
      `para ${email} em ${projectId}.`,
  );
  console.log(
      "O usuário deve sair e entrar novamente para receber um novo ID token.",
  );
}

main().catch((error) => {
  console.error(error.message);
  process.exitCode = 1;
});
