import ForgotPasswordForm from "../../components/auth/ForgotPasswordForm";
import MainLayout from "../../common/layouts/MainLayout";

const ForgotPasswordPage = () => {
  return (
    <MainLayout>
      <div className="py-8">
        <h1 className="text-3xl font-bold text-center mb-8">Forgot Password</h1>
        <ForgotPasswordForm />
      </div>
    </MainLayout>
  );
};

export default ForgotPasswordPage;
