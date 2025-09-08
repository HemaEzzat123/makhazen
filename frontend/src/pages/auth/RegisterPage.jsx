import RegisterForm from "../../components/auth/RegisterForm";
import MainLayout from "../../common/layouts/MainLayout";

const RegisterPage = () => {
  return (
    <MainLayout>
      <div className="py-8">
        <h1 className="text-3xl font-bold text-center mb-8">Create Account</h1>
        <RegisterForm />
      </div>
    </MainLayout>
  );
};

export default RegisterPage;
