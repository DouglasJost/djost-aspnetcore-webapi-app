using OpenAiChatCompletions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAiChatCompletions.VisitNoteTranscripts
{
    public class VisitNoteTranscriptService : IVisitNoteTranscriptService
    {
        public string GetBasicTranscript()
        {
            StringBuilder sbTranscript = new StringBuilder();

            sbTranscript.Append("Doctor: Good morning, Mr. Smith.What brings you in today?");
            sbTranscript.Append("Patient: Morning, Doctor.I’ve been having this persistent cough for about two weeks now.It’s worse at night and it’s been keeping me up.");
            sbTranscript.Append("Doctor: Is it a dry cough or are you producing any phlegm ?");
            sbTranscript.Append("Patient : It started out dry, but in the past few days, I've been coughing up some yellowish mucus.");
            sbTranscript.Append("Doctor: Have you noticed any other symptoms, like fever, shortness of breath, or chest pain ?");
            sbTranscript.Append("Patient : No fever, but I do get winded easily when climbing stairs.I’ve also had some mild chest tightness, especially when I lie down.");
            sbTranscript.Append("Doctor: Have you been around anyone who’s been sick recently?");
            sbTranscript.Append("Patient: Not that I know of, but I did travel last month for work, so I might’ve caught something during the trip.");
            sbTranscript.Append("Doctor: Any allergies, or are you taking any medications ?");
            sbTranscript.Append("Patient : I’m allergic to penicillin, but I’m not on any regular medications.I’ve just been taking over - the - counter cough syrup, but it hasn’t helped much.");
            sbTranscript.Append("Doctor: Do you smoke or have a history of smoking ?");
            sbTranscript.Append(" : No, I don’t smoke.");
            sbTranscript.Append("Doctor: Alright, let me take a listen to your lungs. (Doctor performs lung auscultation) There seems to be some mild wheezing on your right side.I’d like to order a chest X - ray to rule out any infections like pneumonia, and we’ll also test for any possible bacterial causes.");
            sbTranscript.Append("Patient: Sounds good.Do you think it’s serious ?");
            sbTranscript.Append("Doctor : It could just be a lingering infection, but we’ll know more once the tests are done.In the meantime, I’m going to prescribe an inhaler to help with the chest tightness and a stronger cough suppressant.");
            sbTranscript.Append("Patient: Thanks, Doctor.I appreciate it.");

            return sbTranscript.ToString();
        }

        public string GetSeriousTranscript()
        {

            //var transcription = "Good morning, how have you been feeling since your last visit?\n\nWell, to be honest, not great. I've been experiencing some chest pain and shortness of breath, especially when I'm doing any kind of physical activity. \n\nI see. Let's not disregard those symptoms. They can be quite serious, especially considering your family history of heart issues. Are there any other symptoms you're experiencing?\n\nYes, I've been feeling dizzy at times, and I've noticed some swelling in my legs and feet. And then there's the constant pain in my right knee. It's been really bad lately, and it's making it difficult to move around.\n\nI'm sorry to hear about your knee pain. Given your family history of knee bone weakness, it's important we address that as well. Now, before we discuss the pain in your knee further, let's go over your medications. Can you remind me of what you're currently taking?\n\nSure, I'm taking Lisinopril for my blood pressure, Simvastatin for cholesterol, and recently my other doctor prescribed me with a new medication, Warfarin. And for my knee pain, I've been taking Naproxen.\n\nThank you. It's important to be cautious here because taking Warfarin and Naproxen together can increase the risk of bleeding. We'll need to address that. Additionally, combining these with Simvastatin can also sometimes have complications. How about your vitals? Can I confirm them with you?\n\nYes, I'm 5'7\" tall and currently weigh about 250 pounds. My birthdate is March 16, 1970, and I'm a male.\n\nI appreciate that. Given your weight compared to your height, managing your blood pressure and addressing these heart-related symptoms becomes even more significant. We'll need to run some tests to further investigate your chest pains, shortness of breath, and dizziness. This, in combination with your leg swelling, suggests we should thoroughly examine your heart health. \n\nThat sounds like a good plan. I just want to feel better and prevent any more serious issues.\n\nAbsolutely. In the meantime, we may need to reassess your medications, especially the ones that could be interacting, and possibly introduce a different pain management strategy for your knee. Let's start with some blood work and maybe an imaging study for your heart and knee. I'll set up those tests today.\n\nThank you, Doctor. I really appreciate your help.\n\nOf course, we're here to ensure you get back to feeling your best.";
            //return transcription;

            StringBuilder sbTranscript = new StringBuilder();
            sbTranscript.Append("Doctor : Good morning, Mr. Johnson. How are you feeling today?");
            sbTranscript.Append("Patient : Well, I'm not feeling so great, honestly. I'm having some issues that I wanted to talk about.");
            sbTranscript.Append("Doctor : I'm sorry to hear that. Let's take a closer look at what's going on. First, let me confirm your details. You're 55 years old, male, born on March 3rd, 1968, right? Your height is 5 feet 7 inches, and your weight today is 250 pounds. Is that correct?");
            sbTranscript.Append("Patient : Yes, that's correct.");
            sbTranscript.Append("Doctor : Alright. Let's discuss your main concerns today. What symptoms are you experiencing?");
            sbTranscript.Append("Patient : I've been having chest pains and shortness of breath. I also get dizzy sometimes when I stand up. And my right knee is causing major pain, making it hard to walk.");
            sbTranscript.Append("Doctor : Hmm, the chest pain and shortness of breath are certainly concerning, especially with your family history of heart issues. And about your knee, I see there’s a history of bone weakness in your family as well. We need to address these issues seriously. Are you taking any medications currently?");
            sbTranscript.Append("Patient : Yes, I'm taking Warfarin for blood clot prevention, Amlodipine for high blood pressure, and Simvastatin for cholesterol.");
            sbTranscript.Append("Doctor : Those are important medications, but I notice a potential interaction between Warfarin and Amlodipine, which could be contributing to your dizziness and possibly affecting your blood pressure more than we’d like. We need to adjust those to avoid any serious drug interactions.");
            sbTranscript.Append("Patient : I see. That might explain a few things.");
            sbTranscript.Append("Doctor : Given your symptoms, I’m quite concerned about your heart. The chest pain and dizziness, coupled with your family history, suggest we should run some tests to assess your cardiovascular health. I'll schedule an ECG and a stress test to get a clearer picture.");
            sbTranscript.Append("Patient : That sounds necessary. What can we do about my knee pain?");
            sbTranscript.Append("Doctor : For your right knee, it sounds like there might be severe osteoarthritis or possibly a ligament issue. I'll refer you to an orthopedic specialist who can provide more targeted treatment. We should also look at physical therapy to help manage your pain and mobility in the meantime.");
            sbTranscript.Append("Patient : That would be great. It's been really painful and hard to manage on my own.");
            sbTranscript.Append("Doctor : I understand. We'll work together to get you feeling better. In the meantime, please watch your activities to not over-stress your knee, and keep monitoring those symptoms. If you notice any increased chest pain or dizziness, go to the emergency room immediately.");
            sbTranscript.Append("Patient : Will do. Thanks for your help, Doctor.");
            sbTranscript.Append("Doctor : You're welcome. We'll follow up soon after the tests to review your results and adjust your treatment plan. Take care, Mr. Johnson.");

            return sbTranscript.ToString();
        }
    }
}
