import LoginForm from "../../components/auth/LoginForm";
import MainLayout from "../../common/layouts/MainLayout";

const LoginPage = () => {
  return (
    <MainLayout>
      <div className="py-8">
        <h1 className="text-3xl font-bold text-center mb-8">Sign In</h1>
        <LoginForm />
      </div>
    </MainLayout>
  );
};

export default LoginPage;
