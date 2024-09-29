export default [
  {
    files: ["src/**/*.ts", "src/**/*.html"],
    languageOptions: {
      parser: "@typescript-eslint/parser",
      parserOptions: {
        ecmaVersion: 2020,
        sourceType: "module",
        project: "./tsconfig.app.json",
      },
    },
    plugins: ["@typescript-eslint", "prettier"],
    extends: [
      "eslint:recommended",
      "plugin:@typescript-eslint/recommended",
      "plugin:prettier/recommended", // Enables Prettier rules as ESLint rules
    ],
    rules: {
      "prettier/prettier": "error", // Show Prettier formatting issues as errors
      "@typescript-eslint/no-unused-vars": "error", // ESLint rule for unused vars
      quotes: ["error", "single"], // Use single quotes (handled by Prettier)
    },
  },
];
